import React, { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import { Typography, Container, Box, Button, Paper } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { IHttpConnectionOptions } from '@microsoft/signalr';

const HomePage: React.FC = () => {
  const [notifications, setNotifications] = useState<string[]>([]);
  const navigate = useNavigate();
  const user = localStorage.getItem('username');

  useEffect(() => {
    if (!localStorage.getItem('token')) {
      navigate('/login');
      return;
    }
 
    const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7176/notificationHub", {
        accessTokenFactory: async () => {
          return localStorage.getItem('token');
        }
      } as IHttpConnectionOptions)
    .configureLogging(signalR.LogLevel.Information)
    .build();
  

    async function start() {
      try {
        await connection.start();
        console.log("SignalR Connected.");
      } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
      }
    }

    connection.on("ReceiveMessage", (user, message) => {
      console.log("ReceiveMessage", user);
      
      const notification = JSON.parse(message);
      setNotifications((prev) => [...prev, notification.Message]);
    });

    connection.onclose(async () => {
      await start();
    });

    start();
  }, [navigate]);

  return (
    <Container maxWidth="md" sx={{ mt: 4, p: 3 }}>
      <Box
        sx={{
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          backgroundColor: '#f5f5f5',
          borderRadius: '8px',
          p: 3,
          boxShadow: 3,
        }}
      >
        <Button
          variant="contained"
          color="secondary"
          sx={{ alignSelf: 'flex-end' }}
          onClick={() => {
            localStorage.removeItem('token');
            localStorage.removeItem('username');
            navigate('/login');
          }}
        >
          Log out
        </Button>

        <Typography variant="h4" gutterBottom sx={{ textAlign: 'center', fontWeight: 600, mt: 2 }}>
          Home Page
        </Typography>

        {user && <Typography variant="h6" sx={{ marginBottom: 2 }}>Welcome, {user}</Typography>}

        <Paper sx={{ width: '100%', padding: 2, marginBottom: 2, backgroundColor: '#fff', maxHeight: 400, overflowY: 'auto' }}>
          <Typography variant="h6" sx={{ fontWeight: 500 }}>Notifications:</Typography>
          {notifications.length > 0 ? (
            notifications.map((notification, index) => (
              <Typography
                key={index}
                sx={{
                  mt: 1,
                  color: '#333',
                  textOverflow: 'ellipsis',
                  whiteSpace: 'nowrap',
                  overflow: 'hidden',
                  width: '100%'
                }}
              >
                {notification}
              </Typography>
            ))
          ) : (
            <Typography sx={{ color: 'gray' }}>No new notifications.</Typography>
          )}
        </Paper>
      </Box>
    </Container>
  );
}

export default HomePage;

const app = require('./src/app.js');
const { Server } = require('socket.io');
const { createServer } = require("http");
const grpc = require("@grpc/grpc-js");
const NotificationService = require('./src/grpc_services/notification_service.js');
const { NotificationService: NotificationServiceBase } = require('./src/grpc_services/notification_grpc_pb.js'); // Using require

const port = process.env.PORT || 50001;

const httpServer = createServer(app);
const io = new Server(httpServer, {});

io.on("connection", (socket) => {
  const req = socket.request;
  console.log(req);
});
 
httpServer.listen(port, () => {
  console.log(`Server started on port ${port}`);
});

const grpcServer = new grpc.Server();
const notificationService = new NotificationService(grpc);

grpcServer.addService(NotificationServiceBase, notificationService);

grpcServer.bindAsync('localhost'.concat(':').concat(port), grpc.ServerCredentials.createInsecure(), () => {
  console.log("gRPC server running at port", port);
  grpcServer.start();
});

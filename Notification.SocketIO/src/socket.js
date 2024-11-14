const app = require('./app.js')
const { Server } = require('socket.io')
const {createServer} = require('http')

const httpServer = createServer(app)
const io = new Server(httpServer, {})

// Middleware...

module.exports = io

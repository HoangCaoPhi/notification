const grpc = require("@grpc/grpc-js")
const NotificationService = require('./grpc_services/notification_service.js')
const { NotificationService: NotificationServiceBase } = require('./grpc_services/notification_grpc_pb.js')

const grpcServer = new grpc.Server()
const notificationService = new NotificationService(grpc)

grpcServer.addService(NotificationServiceBase, notificationService)

module.exports = grpcServer
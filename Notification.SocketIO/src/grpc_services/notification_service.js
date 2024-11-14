const messages = require('./notification_pb')

module.exports = class NotificationService {
    constructor(grpc) {
        this.grpc = grpc
    }

    sendNotification = (call, callback) => {
        console.log("Received notification request:", call.request);
        const response = new messages.SendNotificationReply();
        callback(null, response)
    }
}
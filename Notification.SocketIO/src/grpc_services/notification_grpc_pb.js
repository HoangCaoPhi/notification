// GENERATED CODE -- DO NOT EDIT!

'use strict';
var grpc = require('@grpc/grpc-js');
var notification_pb = require('./notification_pb.js');

function serialize_SendNotificationReply(arg) {
  if (!(arg instanceof notification_pb.SendNotificationReply)) {
    throw new Error('Expected argument of type SendNotificationReply');
  }
  return Buffer.from(arg.serializeBinary());
}

function deserialize_SendNotificationReply(buffer_arg) {
  return notification_pb.SendNotificationReply.deserializeBinary(new Uint8Array(buffer_arg));
}

function serialize_SendNotificationRequest(arg) {
  if (!(arg instanceof notification_pb.SendNotificationRequest)) {
    throw new Error('Expected argument of type SendNotificationRequest');
  }
  return Buffer.from(arg.serializeBinary());
}

function deserialize_SendNotificationRequest(buffer_arg) {
  return notification_pb.SendNotificationRequest.deserializeBinary(new Uint8Array(buffer_arg));
}


var NotificationService = exports.NotificationService = {
  sendNotification: {
    path: '/Notification/SendNotification',
    requestStream: false,
    responseStream: false,
    requestType: notification_pb.SendNotificationRequest,
    responseType: notification_pb.SendNotificationReply,
    requestSerialize: serialize_SendNotificationRequest,
    requestDeserialize: deserialize_SendNotificationRequest,
    responseSerialize: serialize_SendNotificationReply,
    responseDeserialize: deserialize_SendNotificationReply,
  },
};

exports.NotificationClient = grpc.makeGenericClientConstructor(NotificationService);

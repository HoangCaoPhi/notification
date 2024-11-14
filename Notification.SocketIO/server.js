const app = require('./src/app.js');
const io = require('./src/socket.js')
const grpcServer = require('./src/grpc.js')
const grpc = require("@grpc/grpc-js");
 
const port = process.env.PORT || 50001;

io.on("connection", (socket) => {
  const req = socket.request;
  console.log(req);
});
 
app.listen(port, () => {
  console.log(`Server started on port ${port}`);
});
 
grpcServer.bindAsync('localhost'.concat(':').concat(port), grpc.ServerCredentials.createInsecure(), () => {
  console.log("gRPC server running at port", port);
  grpcServer.start();
});

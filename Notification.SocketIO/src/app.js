const express = require('express')

const app = express()

// middleware...

app.get('/', (req, res) => {
    res.send('Hello, world!');
});

module.exports = app

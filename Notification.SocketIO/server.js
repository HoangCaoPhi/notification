import app from './src/app.js';

const port = process.env.PORT ?? 50001;
 
app.listen(port, () => {
    console.log(`Server start with ${port}`)
})
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import { env } from 'node:process';
 
export default defineConfig({
    plugins: [react()],
    server: {
        proxy: {
            '^/api': {
                target: env.VITE_API,
                secure: false
            }
        },
        port: env.FE_PORT || 5173
    }
})

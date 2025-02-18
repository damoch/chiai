const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7239';

const PROXY_CONFIG = [
  {
    context: ["/chat"], // 👈 This tells Angular to proxy only requests matching "/chat"
    target: target,
    secure: false,
    changeOrigin: true,
    logLevel: "debug", // 👈 Logs detailed proxy behavior in the console
  }
]

module.exports = PROXY_CONFIG;

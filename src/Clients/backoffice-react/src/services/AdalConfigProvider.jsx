class AdalConfigProvider {
  getAdalConfig() {
    const adalConfig = {
      tenant: 'nwdstore2018.onmicrosoft.com',
      clientId: 'bd50fd75-9aae-481a-833b-544c8a5015f8',
      endpoints: {
        api:
          'https://nwdstore2018.onmicrosoft.com/b7cb7c78-9ed2-4aca-8378-7955ae248640',
      },
      postLogoutRedirectUri: window.location.origin,
      redirectUri: 'http://localhost:3000',
      cacheLocation: 'sessionStorage',
    };

    return adalConfig;
  }
}

export default AdalConfigProvider;

// https://itnext.io/a-memo-on-how-to-implement-azure-ad-authentication-using-react-and-net-core-2-0-3fe9bfdf9f36
// https://www.yairripshtos.com/add-aad-authentication-to-react-app/
import { AuthenticationContext } from 'react-adal';
import AdalConfigProvider from './AdalConfigProvider';

class AdalService {
  constructor() {
    adalConfig = new AdalConfigProvider();
    context = new AuthenticationContext(adalConfig.getAdalConfig());
  }

  login() {
    this.context.login();
  }

  logout() {
    this.context.logOut();
  }

  handleCallback() {
    this.context.handleWindowCallback();
  }

  getActiveDirectoryApplicationId() {
    return this.adalConfig.clientId;
  }

  userInfo() {
    return this.context.getCachedUser();
  }

  accessToken() {
    return this.context.getCachedToken(this.adalConfig.clientId);
  }

  isAuthenticated() {
    return this.accessToken && this.userInfo;
  }
}

export default AdalService;

export default class ClientBase {
    constructor(loginClient) {
        this.loginClient = loginClient;
    }

    async fetch(uri) {
        try {
            let requestOptions = this.getRequestOptions();
            let response = await fetch(uri, requestOptions);
            this.verifyResponse(response);
            let info = await response.json();

            return info;
        }
        catch (error) {
            let message = error.messge || error.statusText || error;

            console.error('ClientBase::fetch', uri, error);
            alert(message);
            throw error;
        }
    }

    verifyResponse(response) {
        if (!response.ok) {
            throw response;
        }
    }

    getRequestOptions(method) {
        method = method || process.env.REACT_APP_methodGet;

        let authorizationHeader = this.loginClient.getAuthorizationHeader();
        let headers = new Headers(authorizationHeader);
        headers.append('Content-Type', process.env.REACT_APP_applicationJson);

        let requestOptions = {
            method: method,
            headers: headers
        };

        return requestOptions;
    }
}

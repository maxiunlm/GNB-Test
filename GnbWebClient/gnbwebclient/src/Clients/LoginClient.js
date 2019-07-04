export default class LoginClient {
    constructor() {
        this.user = {};

        this.postSubmit = this.postSubmit.bind(this);
        this.login = this.login.bind(this);
        this.getUser = this.getUser.bind(this);
        this.getToken = this.getToken.bind(this);
        this.getRequestOptions = this.getRequestOptions.bind(this);
        this.getAuthorizationHeader = this.getAuthorizationHeader.bind(this);
    }

    async postSubmit(form, evt) {
        evt.preventDefault();
        return await this.login(form.username.value, form.password.value);
    }

    async login(username, password) {
        let uri = process.env.REACT_APP_loginWebapiEndpoint + 'authenticate';
        let headers = new Headers({
            "Content-Type": "application/json"
        });
        let user = {
            id: null,
            username: username,
            password: password
        };
        let postData = {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(user)
        };

        let response = await fetch(uri, postData);

        if (response.ok === false) {
            console.error(response);

            return false;
        }

        this.user = await response.json();

        return this.user;
    }

    getRequestOptions(method) {
        method = method || process.env.REACT_APP_methodGet
        let authorizationHeader = this.getAuthorizationHeader();
        let headers = new Headers(authorizationHeader);
        headers.append(
            process.env.REACT_APP_contentType,
            process.env.REACT_APP_applicationJson);
        let requestOptions = {
            method: method,
            headers: headers
        };

        return requestOptions;
    }

    getAuthorizationHeader() {
        let token = this.getToken();
        let header = { 'Authorization': 'Bearer ' + token };

        return header;
    }

    getToken() {
        return this.user.token;
    }

    getUser() {
        return this.user;
    }
} 

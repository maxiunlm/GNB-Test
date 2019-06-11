export default class ClientBase {
    async fetch(uri) {
        try {
            let requestOptions = this.getRequestOptions();
            let response = await fetch(uri, requestOptions);
            this.verifyResponse(response);
            let info = await response.json();

            return info;
        }
        catch (error) {
            let message = error.messge || error;

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

    getRequestOptions() {
        let headers = new Headers({
            'Content-Type': process.env.REACT_APP_applicationJson
        });
        let requestOptions = {
            method: process.env.REACT_APP_methodGet,
            headers: headers
        };

        return requestOptions;
    }
}

import ClientBase from './Base/ClientBase';

export default class RatesClient extends ClientBase {
    constructor(loginClient) {
        super(loginClient);

        this.listRates = this.listRates.bind(this);
        this.fetch = this.fetch.bind(this);
        this.verifyResponse = this.verifyResponse.bind(this);
        this.getRequestOptions = this.getRequestOptions.bind(this);
    }

    async listRates() {
        let rates = await this.fetch(process.env.REACT_APP_ratesWebapiEndpoint);

        return rates;
    }
}
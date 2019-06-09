import ClientBase from './Base/ClientBase';

const ratesWebapiEndpoint = 'http://localhost:5000/api/Rate';

export default class RatesClient extends ClientBase {
    constructor() {
        super();

        this.listRates = this.listRates.bind(this);
        this.fetch = this.fetch.bind(this);
        this.verifyResponse = this.verifyResponse.bind(this);
        this.getRequestOptions = this.getRequestOptions.bind(this);
    }

    async listRates() {
        let rates = await this.fetch(ratesWebapiEndpoint);

        return rates;
    }
}
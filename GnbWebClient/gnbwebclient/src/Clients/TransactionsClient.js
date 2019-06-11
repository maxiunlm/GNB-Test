import ClientBase from './Base/ClientBase';

export default class TransactionsClient extends ClientBase {
    constructor() {
        super();

        this.listTransactions = this.listTransactions.bind(this);
        this.fetch = this.fetch.bind(this);
        this.verifyResponse = this.verifyResponse.bind(this);
        this.getRequestOptions = this.getRequestOptions.bind(this);
    }

    async listTransactions() {
        let transactions = await this.fetch(process.env.REACT_APP_transactionsWebapiEndpoint);

        return transactions;
    }
}
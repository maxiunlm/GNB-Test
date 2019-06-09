import ClientBase from './Base/ClientBase';

const transactionsWebapiEndpoint = 'http://localhost:5000/api/Transaction';

export default class TransactionsClient extends ClientBase {
    constructor() {
        super();

        this.listTransactions = this.listTransactions.bind(this);
        this.fetch = this.fetch.bind(this);
        this.verifyResponse = this.verifyResponse.bind(this);
        this.getRequestOptions = this.getRequestOptions.bind(this);
    }

    async listTransactions() {
        let transactions = await this.fetch(transactionsWebapiEndpoint);

        return transactions;
    }
}
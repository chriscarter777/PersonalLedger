import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as TransactionState from '../store/Transactions';

type AdminTransactionsProps =
    TransactionState.TransactionState
    & typeof TransactionState.actionCreators
    & RouteComponentProps<{}>;

export class AdminTransactions extends React.Component<AdminTransactionsProps, {}> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        this.props.requestTransactions();
    }

    componentWillReceiveProps(nextProps: AdminTransactionsProps) {
        // This method runs when incoming props (e.g., route params) change
        this.props.requestTransactions();
    }

    public render() {
        var transactionItems = this.props.transactions.map(function (item) {
            return (
                <li>
                    {item.id} : {item.date} : {item.amount} : {item.category} : {item.drAcct} : {item.crAcct} : {item.tax}
                </li>
            );
        });

        return <div>
            <h1 className="captionlike">Admin-Transactions</h1>

            <ul>
                {transactionItems}
            </ul>

            <button onClick={() => { this.props.addTransaction(this.props.transactions[0]) }}>Add</button>
            <button onClick={() => { this.props.deleteTransaction(0) }}>Delete</button>
            <button onClick={() => { this.props.updateTransaction(this.props.transactions[0]) }}>Edit</button>
        </div>
    }
}

// Wire up the React component to the Redux store
// Selects which state properties are merged into the component's props
export default connect((state: ApplicationState) => state.transactions, TransactionState.actionCreators)(AdminTransactions) as typeof AdminTransactions;
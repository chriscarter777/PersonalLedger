import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState }  from '../store';
import * as AccountStore from '../store/Accounts';
import * as CategoryStore from '../store/Categories';
import * as TransactionStore from '../store/Transactions';

// At runtime, Redux will merge together...
type LedgerProps =
    AccountStore.AccountState
    & CategoryStore.CategoryState
    & TransactionStore.TransactionState
    & typeof AccountStore.actionCreators
    & typeof CategoryStore.actionCreators
    & typeof TransactionStore.actionCreators
    & RouteComponentProps<{ startDateIndex: string }>;

class Ledger extends React.Component<LedgerProps, {}> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        this.props.requestAccounts();
        this.props.requestCategories();
        this.props.requestTransactions();
    }

    //componentWillReceiveProps(nextProps: LedgerProps) {
    //    // This method runs when incoming props (e.g., route params) change
    //    this.props.requestAccounts();
    //    this.props.requestCategories();
    //    this.props.requestTransactions();
    //}

    public render() {
        console.log("-----------------------------------------------");
        console.log("LedgerProps: " + JSON.stringify(this.props));
        console.log("Accounts: " + JSON.stringify(this.props.accounts.length));
        console.log("-----------------------------------------------");

        var greenStyle = { color: 'green' };

        var displayAsDollar = (amt: number) => '$ ' + amt.toFixed(2);

        var transactionItems = this.props.transactions.map((item) =>
                <tr key={item.id.toString()}>
                    <td>{item.id}</td>
                    <td>{item.date}</td>
                    <td className='right'>{displayAsDollar(item.amount)}</td>
                    <td>{item.category}</td>
                    <td>{item.drAcct}</td>
                    <td>{item.crAcct}</td>
                    <td>&nbsp;{item.tax && <span className='glyphicon glyphicon-copy' style={greenStyle}></span>}</td>
                </tr>
        );

        return <div>
            <table>
                <caption>Transactions</caption>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Date</th>
                        <th>Amount</th>
                        <th>Category</th>
                        <th>Debit Account</th>
                        <th>Credit Account</th>
                        <th>Tax?</th>
                    </tr>
                </thead>
                <tbody>
                    {transactionItems}
                </tbody>
           </table>
            <button onClick={() => { this.props.addTransaction(this.props.transactions[0]) }}>Add</button>
            <button onClick={() => { confirm('are you sure you want to delete this category?'); this.props.deleteTransaction(0) }}>Delete</button>
            <button onClick={() => { this.props.updateTransaction(this.props.transactions[0]) }}>Edit</button>
        </div>
    }

    //private renderPagination() {
    //    let prevStartDateIndex = (this.props.startDateIndex || 0) - 5;
    //    let nextStartDateIndex = (this.props.startDateIndex || 0) + 5;

    //    return <p className='clearfix text-center'>
    //        <Link className='btn btn-default pull-left' to={ `/fetchdata/${ prevStartDateIndex }` }>Previous</Link>
    //        <Link className='btn btn-default pull-right' to={ `/fetchdata/${ nextStartDateIndex }` }>Next</Link>
    //        { this.props.isLoading ? <span>Loading...</span> : [] }
    //    </p>;
    //}
}

// Wire up the React component to the Redux store
// Selects which state properties are merged into the component's props
export default connect((state: ApplicationState) => state.accounts, AccountStore.actionCreators)(Ledger) as typeof Ledger;

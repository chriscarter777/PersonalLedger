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

    componentWillReceiveProps(nextProps: LedgerProps) {
        // This method runs when incoming props (e.g., route params) change
        this.props.requestAccounts();
        this.props.requestCategories();
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
            <h1 className="captionlike">Ledger</h1>

            <ul>
                {transactionItems}
            </ul>

            <button onClick={() => { this.props.addTransaction(this.props.transactions[0]) }}>Add</button>
            <button onClick={() => { this.props.deleteTransaction(0) }}>Delete</button>
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

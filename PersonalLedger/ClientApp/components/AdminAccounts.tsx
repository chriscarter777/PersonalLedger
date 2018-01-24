import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as AccountStore from '../store/Accounts';

type AdminAccountsProps =
    AccountStore.AccountState
    & typeof AccountStore.actionCreators
    & RouteComponentProps<{}>;

export class AdminAccounts extends React.Component<AdminAccountsProps, {}> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        this.props.requestAccounts();
    }

    componentWillReceiveProps(nextProps: AdminAccountsProps) {
        // This method runs when incoming props (e.g., route params) change
        this.props.requestAccounts();
    }

    public render() {
        var accountItems = this.props.accounts.map(function (item) {
            return (
                <li>
                    {item.debit} : {item.id} : {item.name} : {item.institution} : {item.number} : {item.interest} : {item.limit}
                </li>
            );
        });

        return <div>
            <h1 className="captionlike">Admin-Accounts</h1>

            <ul>
                {accountItems}
            </ul>

            <button onClick={() => { this.props.addAccount(this.props.accounts[0]) }}>Add</button>
            <button onClick={() => { this.props.deleteAccount(0) }}>Delete</button>
            <button onClick={() => { this.props.updateAccount(this.props.accounts[0]) }}>Edit</button>
            <button onClick={() => { this.props.updateDefaults(0, 0, 0, 0) }}>Change Defaults</button>
        </div>;
    }
}

// Wire up the React component to the Redux store
// Selects which state properties are merged into the component's props
export default connect((state: ApplicationState) => state.accounts, AccountStore.actionCreators)(AdminAccounts) as typeof AdminAccounts;
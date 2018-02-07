import * as React from 'react';
import { Route, Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as AccountStore from '../store/Accounts';
import { Account } from '../store/Accounts';
import { AccountAdd } from './AccountAdd';
import { AccountItem } from './AccountItem';

type AccountsProps =
    AccountStore.AccountState
    & typeof AccountStore.actionCreators
    & RouteComponentProps<{}>;

export class Accounts extends React.Component<AccountsProps, {}> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        this.props.requestAccounts();
    }

    componentWillReceiveProps(nextProps: AccountsProps) {
        // This method runs when incoming props (e.g., route params) change
    }

    public render() {
        return <div>
                <table>
                    <caption>Accounts</caption>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Type</th>
                            <th>Name</th>
                            <th>Institution</th>
                            <th>Number</th>
                            <th>Interest</th>
                            <th>Limit</th>
                            <th>Balance</th>
                        </tr>
                    </thead>
                    <tbody>
                    {this.props.accounts.map((item, i) =>
                        <AccountItem key={i} account={item} />
                    )}
                    </tbody>
                </table>
                <hr />
                <Link to='/accountAdd'>Add</Link>
                <hr />
                <Route path='/accountAdd' component={AccountAdd} />
            </div>;
    }
}

// Wire up the React component to the Redux store
// Selects which state properties are merged into the component's props
export default connect((state: ApplicationState) => state.accounts, AccountStore.actionCreators)(Accounts) as typeof Accounts;

//                <Route path='/accountAdd' component={AccountAdd} />

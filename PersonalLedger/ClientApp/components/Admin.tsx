import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { NavLink, Link } from 'react-router-dom';
import AdminAccounts from './AdminAccounts';
import AdminCategories from './AdminCategories';
import AdminTransactions from './AdminTransactions';
import { Route } from 'react-router';

//type AdminAccountsProps =
//    AccountStore.AccountState
//    & typeof AccountStore.actionCreators
//    & RouteComponentProps<{}>;

export default class Admin extends React.Component<RouteComponentProps<{}>, {}> {

   public render() {
        return <div className='container-fluid'>
                    <h1 className="captionlike">Administration</h1>
                    <div className='row'>
                        <div className='col-sm-12'>
                    <div className='adminNav'>
                        <ul>
                            <li>
                                <NavLink to={'/admin/accounts'} activeClassName='active'>
                                    <span className='glyphicon glyphicon-credit-card'></span> Accounts
                        </NavLink>
                            </li>
                            <li>
                                <NavLink to={'/admin/categories'} activeClassName='active'>
                                    <span className='glyphicon glyphicon-folder-open'></span> Categories
                        </NavLink>
                            </li>
                            <li>
                                <NavLink to={'/admin/transactions'} activeClassName='active'>
                                    <span className='glyphicon glyphicon-list-alt'></span> Transactions
                        </NavLink>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>;
    }
}

// Wire up the React component to the Redux store
// Selects which state properties are merged into the component's props
//export default connect((state: ApplicationState) => state.accounts, AccountStore.actionCreators)(Admin) as typeof Admin;
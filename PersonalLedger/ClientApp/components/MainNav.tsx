import * as React from 'react';
import { NavLink, Link } from 'react-router-dom';

export class MainNav extends React.Component<{}, {}> {
    public render() {
        return <div className='main-nav'>
                <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={ '/' }>PersonalLedger</Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            <NavLink exact to={'/Home/Main'} activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Home
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={ '/accounts' } activeClassName='active'>
                                <span className='glyphicon glyphicon-credit-card'></span> Accounts
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={ '/categories' } activeClassName='active'>
                                <span className='glyphicon glyphicon-folder-open'></span> Categories
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={ '/transactions' } activeClassName='active'>
                                <span className='glyphicon glyphicon-list-alt'></span> Transactions
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </div>;
    }
}

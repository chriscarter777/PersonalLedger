import * as React from 'react';
import { NavLink, Link } from 'react-router-dom';

export class AdminNav extends React.Component<{}, {}> {
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
                    <Link className='navbar-brand' to={'/'}>PersonalLedger</Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            <a href='/Home/Main'>
                                <span className='glyphicon glyphicon-home'></span> Main Home
                            </a>
                        </li>
                        <li>
                            <NavLink exact to={'/admin'} activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Admin Home
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/admin/users'} activeClassName='active'>
                                <span className='glyphicon glyphicon-user'></span> Users
                            </NavLink>
                        </li>
                   </ul>
                </div>
            </div>
        </div>;
    }
}

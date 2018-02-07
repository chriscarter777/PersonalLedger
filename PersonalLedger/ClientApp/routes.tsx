import * as React from 'react';
import { Route } from 'react-router-dom';
import MainHome from './components/MainHome';
import Accounts from './components/Accounts';
import Categories from './components/Categories';
import Ledger from './components/Ledger';
import AccountAdd from './components/AccountAdd';
import AccountEdit from './components/AccountEdit';

import AdminHome from './components/AdminHome';
import AdminUsers from './components/Users';

export const mainRoutes = <div>
    <Route path='/Home/Main' component={MainHome} />
    <Route path='/ledger' component={Ledger} />
    <Route path='/accounts' component={Accounts} />
    <Route path='/categories' component={Categories} />
</div>;

export const adminRoutes = <div>
    <Route exact path='/admin' component={AdminHome} />
    <Route path='/admin/users' component={AdminUsers} />
</div>;


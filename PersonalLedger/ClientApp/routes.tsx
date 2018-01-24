 import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home';
import Accounts from './components/Accounts';
import Admin from './components/Admin';
import AdminAccounts from './components/AdminAccounts';
import AdminCategories from './components/AdminCategories';
import AdminTransactions from './components/AdminTransactions';
import AdminUsers from './components/AdminUsers';
import Categories from './components/Categories';
import Ledger from './components/Ledger';

export const layoutAndRoutes = <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/ledger' component={Ledger} />
    <Route path='/accounts' component={Accounts} />
    <Route path='/categories' component={Categories} />
    <Route exact path='/admin' component={Admin} />
</Layout>;

export const routes =<div>
    <Route exact path='/' component={Home} />
    <Route path='/ledger' component={Ledger} />
    <Route path='/accounts' component={Accounts} />
    <Route path='/categories' component={Categories} />
    <Route exact path='/admin' component={Admin} />
    <Route path='/admin/accounts' component={AdminAccounts} />
    <Route path='/admin/categories' component={AdminCategories} />
    <Route path='/admin/transactions' component={AdminTransactions} />
    <Route path='/admin/users' component={AdminUsers} />
</div>;


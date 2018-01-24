import * as Accounts from './Accounts';
import * as Categories from './Categories';
import * as Transactions from './Transactions';
import * as Users from './Users';

// The top-level state object
export interface ApplicationState {
    accounts: Accounts.AccountState;
    categories: Categories.CategoryState;
    transactions: Transactions.TransactionState;
    users: Users.UserState;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    accounts: Accounts.reducer,
    categories: Categories.reducer,
    transactions: Transactions.reducer,
    users: Users.reducer
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}

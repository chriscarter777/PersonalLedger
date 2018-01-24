import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

export interface User {
    id: number;
    userName: string;
    claims: string[];
    roles: string[];
}

export interface UserState {
    isLoading: boolean;
    users: User[];
}

const unloadedState: UserState = { users: [], isLoading: false };

interface RequestUsersAction { type: 'REQUEST_USERS' }
interface ReceiveUsersAction { type: 'RECEIVE_USERS'; users: User[] }
interface AddUserAction { type: 'ADD_USER'; user: User }
interface DeleteUserAction { type: 'DELETE_USER'; id: number }
interface AddClaimAction { type: 'ADD_CLAIM'; id: number; claim: string }
interface DeleteClaimAction { type: 'DELETE_CLAIM'; id: number; claimId: number }
interface AddRoleAction { type: 'ADD_ROLE'; id: number; role: string }
interface DeleteRoleAction { type: 'DELETE_ROLE'; id: number; roleId: number }

type KnownAction = RequestUsersAction | ReceiveUsersAction | AddUserAction | DeleteUserAction | AddClaimAction | DeleteClaimAction | AddRoleAction | DeleteRoleAction;

export const actionCreators = {
    requestUsers: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's not already loading
        if (getState().users.isLoading === false) {
            let fetchTask = fetch('api/Users/UsersAsync')
                .then(response => response.json() as Promise<User[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_USERS', users: data });
                });

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: 'REQUEST_USERS' });
        }
    },
    addUser: (user: User) => <AddUserAction>{ type: 'ADD_USER', user: user },
    deleteUser: (userId: number) => <DeleteUserAction>{ type: 'DELETE_USER',id: userId },
    addClaim: (userId: number, claim: string) => <AddClaimAction>{ type: 'ADD_CLAIM', id: userId, claim: claim },
    deleteClaim: (userId: number, claimid: number) => <DeleteClaimAction>{ type: 'DELETE_CLAIM', id: userId, claimId: claimid },
    addRole: (userId: number, role: string) => <AddRoleAction>{ type: 'ADD_ROLE', id: userId, role: role },
    deleteRole: (userId: number, roleid: number) => <DeleteRoleAction>{ type: 'DELETE_ROLE', id: userId, roleId: roleid }
};

export const reducer: Reducer<UserState> = (state: UserState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_USERS':
            return { isLoading: true, users: state.users };
        case 'RECEIVE_USERS':
            return { isLoading: false, users: action.users };
        case 'ADD_USER':
            return { isLoading: state.isLoading, users: state.users };
        case 'DELETE_USER':
            return { isLoading: state.isLoading, users: state.users };
        case 'ADD_CLAIM':
            return { isLoading: state.isLoading, users: state.users };
        case 'DELETE_CLAIM':
            return { isLoading: state.isLoading, users: state.users };
        case 'ADD_ROLE':
            return { isLoading: state.isLoading, users: state.users };
        case 'DELETE_ROLE':
            return { isLoading: state.isLoading, users: state.users };
        default:
            // The following line guarantees that every action has been covered by a case
            const exhaustiveCheck: never = action;
    }

    // For unrecognized actions (or in cases where actions have no effect), must return the existing state
    //  (or default initial state if none was supplied)
    return state || unloadedState;
};

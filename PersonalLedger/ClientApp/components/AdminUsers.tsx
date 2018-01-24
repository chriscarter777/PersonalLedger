import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as UserStore from '../store/Users';

type AdminUsersProps =
    UserStore.UserState
    & typeof UserStore.actionCreators
    & RouteComponentProps<{}>;

export class AdminUsers extends React.Component<AdminUsersProps, {}> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        this.props.requestUsers();
    }

    componentWillReceiveProps(nextProps: AdminUsersProps) {
        // This method runs when incoming props (e.g., route params) change
        this.props.requestUsers();
    }

    public render() {
        var UserItems = this.props.users.map(function (item) {
            return (
                <li>
                    {item.id} : {item.userName} : {item.claims} : {item.roles}
                </li>
            );
        });

        return <div>
            <h1 className="captionlike">Admin-Users</h1>

            <ul>
                {UserItems}
            </ul>

            <button onClick={() => { this.props.addUser(this.props.users[0]) }}>Add</button>
            <button onClick={() => { this.props.deleteUser(0) }}>Delete</button>
            <button onClick={() => { this.props.addClaim(0, '') }}>Add Claim</button>
            <button onClick={() => { this.props.deleteClaim(0, 0) }}>Delete Claim</button>
            <button onClick={() => { this.props.addRole(0, '') }}>Add Role</button>
            <button onClick={() => { this.props.deleteRole(0, 0) }}>Delete Role</button>
        </div>;
    }
}

// Wire up the React component to the Redux store
// Selects which state properties are merged into the component's props
export default connect((state: ApplicationState) => state.users, UserStore.actionCreators)(AdminUsers) as typeof AdminUsers;
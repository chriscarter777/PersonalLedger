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

    //componentWillReceiveProps(nextProps: AdminUsersProps) {
    //    // This method runs when incoming props (e.g., route params) change
    //    this.props.requestUsers();
    //}

    //<td>{item.admin ? <span className='glyphicon glyphicon-king'></span> : <span>&nbsp;</span>}</td>
    //<td><a onClick={() => { this.props.makeAdmin(item.id) }}>Make Admin</a></td>
    //<td><a onClick={() => { this.props.removeAdmin(item.id) }}>Remove Admin</a></td>


public render() {
        var userItems = this.props.users.map((item) =>
            <tr key={item.userName}>
                <td>{item.id}</td>
                <td>{item.userName}</td>
                <td><input type='checkbox' checked={item.admin} onChange={() => { this.props.toggleAdmin(item.id) }} /></td>
                <td className='deleteLink'><a onClick={() => { confirm('Are you sure you want to delete this user?'); this.props.deleteUser(item.id) }}>Delete</a></td>
            </tr>
        );

        return <div>
            <table>
                <caption>Users</caption>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Username</th>
                        <th>Admin</th>
                    </tr>
                </thead>
                <tbody>
                    {userItems}
                </tbody>
            </table>
            <button onClick={() => { this.props.addUser(this.props.users[0]) }}>Add</button>
        </div>;
    }
}

// Wire up the React component to the Redux store
// Selects which state properties are merged into the component's props
export default connect((state: ApplicationState) => state.users, UserStore.actionCreators)(AdminUsers) as typeof AdminUsers;
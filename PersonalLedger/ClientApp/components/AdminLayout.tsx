import * as React from 'react';
import { AdminNav } from './AdminNav';

export class AdminLayout extends React.Component<{}, {}> {
    public render() {
        return <div className='container-fluid'>
            <div className='row'>
                <div className='col-sm-2'>
                    <AdminNav />
                </div>
                <div className='col-sm-10 content-area'>
                    {this.props.children}
                </div>
            </div>
        </div>;
    }
}

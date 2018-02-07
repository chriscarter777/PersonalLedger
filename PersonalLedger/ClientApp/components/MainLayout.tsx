import * as React from 'react';
import { MainNav } from './MainNav';

export class MainLayout extends React.Component<{}, {}> {
    public render() {
        return <div className='container-fluid'>
            <div className='row'>
                <div className='col-sm-2'>
                    <MainNav />
                </div>
                <div className='col-sm-10 content-area'>
                    { this.props.children }
                </div>
            </div>
        </div>;
    }
}

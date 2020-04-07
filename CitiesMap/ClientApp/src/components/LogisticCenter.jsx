import React, { Component } from 'react';

export class LogisticCenter extends Component {
    constructor(props) {
        super(props);
        this.state = {
            center: {}
        }

        this.handleNewCenterClick = this.handleNewCenterClick.bind(this);
    }


    componentDidMount() {
        this.getCenterData();
    }

    handleNewCenterClick() {
        fetch('LogisticCenter', {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }

        }).then(response => response.json()).then(data => {
            console.log(data);
            this.setState({ center: data })

        });
    }


    render() {
        return (
            <div className="content-container">
                <h4>Logistics Center</h4>
                <table className='table table-striped table-hover table-condensed'>
                    <thead>
                        <tr>
                            <th>Name</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{this.state.center.name}</td>
                        </tr>
                    </tbody>
                </table>
                <div class="btn-wrapper">
                    <button type="button" className="btn btn-light new-btn" onClick={() => this.handleNewCenterClick()}>Generate Logistics Center</button>
                </div>
            </div>
        );
    }

    async getCenterData() {
        fetch('GetCenter', {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }

        }).then(response => response.json()).then(data => {
            this.setState({ center: data })

        });
    }
}

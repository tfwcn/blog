import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as actions from '../common/actions'

class Text extends React.Component {
    constructor(props) {
        super(props);
        this.handleChange = this.handleChange.bind(this);
    }
    handleChange(event) {
        event.preventDefault();
        this.actions.setValue({ value: event.target.value });
        this.props.onChange(this.props.value);
    }
    // 渲染
    render() {
        return (<input type="text" className={this.props.className} value={this.props.value} onChange={this.handleChange} />);
    }
}
Text.propTypes = {
    value: PropTypes.object,
};

/* istanbul ignore next */
function mapStateToProps(state) {
    return {
        value: state.value,
    };
}

/* istanbul ignore next */
function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators({ ...actions }, dispatch),
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Text);
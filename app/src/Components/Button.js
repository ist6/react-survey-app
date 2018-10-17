import React, { Component } from 'react'

export class Button extends Component {
  render() {
    const buttonText = this.props.buttonText;
    const buttonClassName = this.props.className;
    const iconClassName = this.props.icon;
    return (
      <button type="button" className={`btn ${buttonClassName}`}><i className={`fa ${iconClassName}`}></i>{buttonText}</button>
    )
  }
}

export default Button;

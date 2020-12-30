import React from "react";
import {Button, Form, Input} from "antd";
import {LockOutlined, MailOutlined, UserOutlined} from "@ant-design/icons";
import {Link} from "react-router-dom"
import {css} from "styled-components"

const test = css`margin-bottom: 0`



const SignUpPage: React.FC = () => {
    console.log(test)

    return (<>
        <div className="sign-in-page_header">
            <h1>Register Account</h1>
            <p>Feature rich self hosted localization system.</p>
        </div>
        <Form name="normal_sign-in" className="sign-in-form">
            <Form.Item name="username" rules={[{required: true, message: 'Please input your Username!'}]}>
                <Input prefix={<UserOutlined className="site-form-item-icon"/>} placeholder="Username"/>
            </Form.Item>
            <Form.Item name="email" rules={[{required: true, message: 'Please input your email!'}]}>
                <Input prefix={<MailOutlined className="site-form-item-icon"/>} placeholder="user@email.com"/>
            </Form.Item>
            <Form.Item name="password" rules={[{required: true, message: 'Please input your Password!'}]}>
                <Input
                    prefix={<LockOutlined className="site-form-item-icon"/>}
                    type="password"
                    placeholder="Password"/>
            </Form.Item>
            <Form.Item
                name="password-confirm"
                rules={[
                    {required: true, message: 'Please input password corfirm!'}
                ]}>
                <Input
                    prefix={<LockOutlined className="site-form-item-icon"/>}
                    type="password"
                    placeholder="Password confirm"/>
            </Form.Item>
            <Form.Item>
                <Button type="primary" htmlType="submit" block>Register new account</Button>
            </Form.Item>
            <Form.Item noStyle>
                <Link to="/auth/sign-in">
                    <Button type="ghost" htmlType="button" block>Back to sign in page</Button>
                </Link>
            </Form.Item>
        </Form>
    </>)
}

export default SignUpPage;

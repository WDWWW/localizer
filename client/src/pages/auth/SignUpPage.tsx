import React from 'react'
import { Button, Form, Input } from 'antd'
import { LockOutlined, MailOutlined, UserOutlined } from '@ant-design/icons'
import { Link, useHistory } from 'react-router-dom'

const SignUpPage: React.FC = () => {
	const history = useHistory()

	return (
		<>
			<div>
				<h1>Register Account</h1>
				<p>Feature rich self hosted localization system.</p>
			</div>
			<Form name="normal_sign-in">
				<Form.Item name="username" rules={[{ required: true, message: 'Please input your Username!' }]}>
					<Input prefix={<UserOutlined />} placeholder="Username" />
				</Form.Item>
				<Form.Item name="email" rules={[{ required: true, message: 'Please input your email!' }]}>
					<Input prefix={<MailOutlined />} placeholder="user@email.com" />
				</Form.Item>
				<Form.Item name="password" rules={[{ required: true, message: 'Please input your Password!' }]}>
					<Input prefix={<LockOutlined />} type="password" placeholder="Password" />
				</Form.Item>
				<Form.Item
					name="password-confirm"
					rules={[{ required: true, message: 'Please input password corfirm!' }]}
				>
					<Input prefix={<LockOutlined />} type="password" placeholder="Password confirm" />
				</Form.Item>
				<Form.Item>
					<Button type="primary" htmlType="submit" block>
						Register new account
					</Button>
				</Form.Item>
				<Form.Item noStyle>
					<Link to="/auth/sign-in">
						<Button type="ghost" htmlType="button" block>
							Back to sign in page
						</Button>
					</Link>
				</Form.Item>
			</Form>
		</>
	)
}

export default SignUpPage

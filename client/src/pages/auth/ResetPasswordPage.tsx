import { FC } from 'react'
import { Button, Form, Input, Result, Steps } from 'antd'
import { MailOutlined, RetweetOutlined, SmileOutlined, SolutionOutlined } from '@ant-design/icons'
import styled from 'styled-components'

const ResetPasswordPageWrap = styled.div`
	width: 600px;
`

const StepWrap = styled.div`
	margin-bottom: 2rem;
`
const ResetPasswordPage: FC = () => {
	return (
		<ResetPasswordPageWrap>
			<StepWrap>
				<Steps size="small">
					<Steps.Step icon={<MailOutlined />} title="Confirm Email" />
					<Steps.Step icon={<SolutionOutlined />} title="Verification Code" />
					<Steps.Step icon={<RetweetOutlined />} title="Reset password" />
					<Steps.Step icon={<SmileOutlined />} title="Done" />
				</Steps>
			</StepWrap>

			<Form layout={'vertical'} hidden>
				<Form.Item name="email" rules={[{ required: true, message: 'Please input your email' }]}>
					<Input placeholder="Input your email address for verifying your identity." type="email" />
				</Form.Item>
				<Form.Item style={{ marginBottom: 0 }}>
					<Button type="primary" htmlType="button" block>
						Send verification code
					</Button>
				</Form.Item>
			</Form>

			<Form layout={'vertical'} hidden>
				<Form.Item name="email" rules={[{ required: true, message: 'Please input your email' }]}>
					<Input placeholder="user@email.com" type="text" />
				</Form.Item>
				<Form.Item style={{ marginBottom: 0 }}>
					<Button type="primary" htmlType="button" block>
						Resend verification code to user@email.com
					</Button>
				</Form.Item>
			</Form>

			<Form layout={'vertical'} hidden>
				<Form.Item name="new-password" rules={[{ required: true, message: 'Please input your password' }]}>
					<Input placeholder="New password" type="password" />
				</Form.Item>
				<Form.Item
					name="confirm-password"
					rules={[
						{ required: true, message: 'Please input your confirm password as same as above your input' },
					]}
				>
					<Input placeholder="Confirm password" type="password" />
				</Form.Item>
				<Form.Item style={{ marginBottom: 0 }}>
					<Button type="primary" htmlType="button" block>
						Change password
					</Button>
				</Form.Item>
			</Form>

			<Result
				status="success"
				title="Successfully change your password"
				extra={[
					<Button type="primary" key="sign-in">
						Login
					</Button>,
				]}
			/>
		</ResetPasswordPageWrap>
	)
}

export default ResetPasswordPage

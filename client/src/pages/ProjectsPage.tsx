import React from 'react'
import { Badge, Button, Card, Col, Divider, Input, Row } from 'antd'
import styled from 'styled-components'
import Meta from 'antd/es/card/Meta'
import { PlusOutlined } from '@ant-design/icons'
import { ColProps } from 'antd/es/grid'

const ProjectsWrap = styled.div`
	padding: 50px;
	background: white;
	height: 100%;
`

const ProjectsPage: React.FC = () => {
	const projects = [
		{
			role: 'admin',
			name: 'sample project',
			description: 'some description for sample project',
			lastUpdate: new Date(2020, 12, 31),
		},
		{
			role: 'translator',
			name: 'sample project',
			description: 'some description for sample project',
			lastUpdate: new Date(2020, 12, 31),
		},
		{
			role: 'viewer',
			name: 'sample project',
			description: 'some description for sample project',
			lastUpdate: new Date(2020, 12, 31),
		},
	]

	const columnSize: Partial<ColProps> = {
		xs: 24,
		sm: 12,
		md: 8,
		lg: 6,
		xl: 4,
	}

	return (
		<>
			<ProjectsWrap>
				<h2>My projects</h2>
				<div>
					<Input placeholder="Input your project name." />
				</div>
				<Divider />
				<div>
					<Row gutter={[16, 16]}>
						{projects.map(({ name, description, role }, index) => (
							<Col {...columnSize} key={index}>
								{role === 'admin' && (
									<Badge.Ribbon text={role}>
										<Card hoverable style={{ minHeight: '120px' }}>
											<Meta title={name} description={description} />
										</Card>
									</Badge.Ribbon>
								)}
								{role !== 'admin' && (
									<Card hoverable style={{ minHeight: '120px' }}>
										<Meta title={name} description={description} />
									</Card>
								)}
							</Col>
						))}

						<Col {...columnSize}>
							<Button block style={{ height: '100%', minHeight: '120px' }} icon={<PlusOutlined />}>
								Create new project
							</Button>
						</Col>
					</Row>
				</div>
			</ProjectsWrap>
		</>
	)
}

export default ProjectsPage

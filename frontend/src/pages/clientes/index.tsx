import React from 'react';
import './index.css'
import { Button, Col, Row, Space, Table, Tag } from 'antd';
import type { ColumnsType } from 'antd/lib/table';
import { ClienteStatus, GetClienteStatusLabel } from '../../enums/cliente'
import { EditOutlined, EyeOutlined, DeleteOutlined } from '@ant-design/icons';
import Search from 'antd/lib/input/Search';
import Title from 'antd/lib/typography/Title';
import ClienteModal from '../../components/modals/cliente-modal';

interface ClienteType {
    id: number;
    nome: string;
    cpf: string;
    dataNascimento: Date;
    status: ClienteStatus
}

const GetColorLabel = (status: ClienteStatus): string => {
    switch (status) {
        case ClienteStatus.ComPedencias:
            return 'yellow'
        case ClienteStatus.ComPedenciasAtrasadas:
            return 'volcano'
        default:
            return 'green'
    }
}

const columns: ColumnsType<ClienteType> = [
    {
        title: 'Id',
        dataIndex: 'id',
        key: 'id',
    },
    {
        title: 'Nome',
        dataIndex: 'nome',
    },
    {
        title: 'CPF',
        dataIndex: 'cpf',
    },
    {
        title: 'Data de nascimento',
        dataIndex: 'dataNascimento',
        render: (_, { dataNascimento }) => dataNascimento.toLocaleDateString()
    },
    {
        title: 'Status',
        dataIndex: 'status',
        render: (_, { status }) => (
            <Tag color={GetColorLabel(status)}>
                {GetClienteStatusLabel(status)}
            </Tag>
        )
    },
    {
        title: 'Ações',
        dataIndex: 'acao',
        render: () => (
            <Space size="middle">
                <EditOutlined className='actions' />
                {/* <EyeOutlined className='actions' /> */}
                <DeleteOutlined className='actions' style={{ color: 'red' }} />
            </Space>
        )
    },
];

const data: ClienteType[] = [
    {
        id: 1,
        nome: 'John Brown',
        cpf: '107.665.819-93',
        dataNascimento: new Date(),
        status: ClienteStatus.ComPedencias,
    },
    {
        id: 2,
        nome: 'Luiz',
        cpf: '107.665.819-96',
        dataNascimento: new Date(),
        status: ClienteStatus.SemPendencias,
    },
    {
        id: 3,
        nome: 'Joao',
        cpf: '107.665.859-93',
        dataNascimento: new Date(),
        status: ClienteStatus.ComPedenciasAtrasadas,
    },
    {
        id: 4,
        nome: 'fernando',
        cpf: '127.665.819-96',
        dataNascimento: new Date(),
        status: ClienteStatus.SemPendencias,
    },

];

const Clientes: React.FC = () => {
    return (
        <>
            <Row justify='start'>
                <Col >
                    <Title>Clientes</Title>
                </Col>
            </Row>
            <Row justify='space-between' align='middle' className='gutter-box'>
                <Col>
                    <Search placeholder="Buscar" enterButton="Pesquisar" />
                </Col>
                <Col>
                    <ClienteModal />
                </Col>
            </Row>
            <Row justify='center' align='middle' className='gutter-box'>
                <Col span={24}>
                    <Table columns={columns} dataSource={data} scroll={{ x: 800, y: 450 }} />
                </Col>
            </Row>
        </>
    )
};

export default Clientes;
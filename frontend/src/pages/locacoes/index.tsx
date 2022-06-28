import React from 'react';
import './index.css'
import { Button, Col, Row, Space, Table, Tag } from 'antd';
import type { ColumnsType } from 'antd/lib/table';
import { LocacaoStatus, GetLocacaoStatusLabel } from '../../enums/locacao'
import { EditOutlined, EyeOutlined, DeleteOutlined } from '@ant-design/icons';
import Search from 'antd/lib/input/Search';
import Title from 'antd/lib/typography/Title';
import LocacaoModal from '../../components/modals/locacao-modal';

const GetColorLabel = (status: LocacaoStatus): string => {
    switch (status) {
        case LocacaoStatus.Devolvido:
            return 'green'
        case LocacaoStatus.Atrasado:
            return 'volcano'
        default:
            return 'yellow'
    }
}

interface LocacaoType {
    id: number;
    cliente: string;
    filme: string;
    dataLocacao: Date;
    dataDevolucao: Date;
    status: LocacaoStatus;
}

const columns: ColumnsType<LocacaoType> = [
    {
        title: 'Id',
        dataIndex: 'id',
        key: 'id',
    },
    {
        title: 'Cliente',
        dataIndex: 'cliente',
    },
    {
        title: 'Filme',
        dataIndex: 'filme',
    },
    {
        title: 'Data Locação',
        dataIndex: 'dataLocacao',
        render: (_, { dataLocacao }) => dataLocacao.toLocaleDateString()
    },
    {
        title: 'Data Devolução',
        dataIndex: 'dataDevolucao',
        render: (_, { dataDevolucao }) => dataDevolucao.toLocaleDateString()
    },
    {
        title: 'Status',
        dataIndex: 'status',
        render: (_, { status }) => (
            <Tag color={GetColorLabel(status)}>
                {GetLocacaoStatusLabel(status)}
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

const data: LocacaoType[] = [
    {
        id: 1,
        cliente: 'John Brown',
        filme: 'Capitao america',
        dataLocacao: new Date(),
        dataDevolucao: new Date(),
        status: LocacaoStatus.Andamento,
    },
    {
        id: 2,
        cliente: 'Luiz',
        filme: 'The Batman',
        dataLocacao: new Date(),
        dataDevolucao: new Date(),
        status: LocacaoStatus.Atrasado,
    },
    {
        id: 3,
        cliente: 'Monica',
        filme: 'Homem de ferro',
        dataLocacao: new Date(),
        dataDevolucao: new Date(),
        status: LocacaoStatus.Andamento,
    },
    {
        id: 4,
        cliente: 'Tiane',
        filme: 'Harry Potter',
        dataLocacao: new Date(),
        dataDevolucao: new Date(),
        status: LocacaoStatus.Devolvido,
    },
];

const Locacoes: React.FC = () => {
    return (
        <>
            <Row justify='start'>
                <Col >
                    <Title>Locações</Title>
                </Col>
            </Row>
            <Row justify='space-between' align='middle' className='gutter-box'>
                <Col>
                    <Search placeholder="Buscar" enterButton="Pesquisar" />
                </Col>
                <Col>
                    <LocacaoModal />
                </Col>
            </Row>
            <Row justify='center' align='middle' className='gutter-box'>
                <Col span={24}>
                    <Table columns={columns} dataSource={data} scroll={{ x: 800, y:450 }}/>
                </Col>
            </Row>
        </>
    )
};

export default Locacoes;
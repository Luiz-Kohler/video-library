import React from 'react';
import './index.css'
import { Button, Col, Row, Space, Table, Tag } from 'antd';
import type { ColumnsType } from 'antd/lib/table';
import { CheckOutlined, CloseOutlined, EditOutlined, EyeOutlined, DeleteOutlined, ImportOutlined } from '@ant-design/icons';
import Search from 'antd/lib/input/Search';
import Title from 'antd/lib/typography/Title';
import FilmeModal from '../../components/modals/filme-modal'

interface FilmeType {
    id: number;
    titulo: string;
    classificacao: number;
    lancamento: boolean;
}

const columns: ColumnsType<FilmeType> = [
    {
        title: 'Id',
        dataIndex: 'id',
        key: 'id',
    },
    {
        title: 'Titulo',
        dataIndex: 'titulo',
    },
    {
        title: 'Classificação indicativa',
        dataIndex: 'classificacao',
    },
    {
        title: 'Lançamento',
        dataIndex: 'lancamento',
        render: (_, { lancamento }) => lancamento
            ? <CheckOutlined style={{ color: 'green' }} />
            : <CloseOutlined style={{ color: 'red' }} />
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

const data: FilmeType[] = [
    {
        id: 1,
        titulo: 'HOMEM ARANHA',
        classificacao: 10,
        lancamento: true
    },
    {
        id: 2,
        titulo: 'THE BATMAN',
        classificacao: 14,
        lancamento: true
    },
    {
        id: 3,
        titulo: 'CAPITAO AMERICA',
        classificacao: 10,
        lancamento: false
    },
    {
        id: 4,
        titulo: 'HOMEM DE FERRO',
        classificacao: 8,
        lancamento: false
    },
    {
        id: 1,
        titulo: 'HOMEM ARANHA',
        classificacao: 10,
        lancamento: true
    },
    {
        id: 2,
        titulo: 'THE BATMAN',
        classificacao: 14,
        lancamento: true
    },
    {
        id: 3,
        titulo: 'CAPITAO AMERICA',
        classificacao: 10,
        lancamento: false
    },
    {
        id: 4,
        titulo: 'HOMEM DE FERRO',
        classificacao: 8,
        lancamento: false
    },
    {
        id: 1,
        titulo: 'HOMEM ARANHA',
        classificacao: 10,
        lancamento: true
    },
    {
        id: 2,
        titulo: 'THE BATMAN',
        classificacao: 14,
        lancamento: true
    },
    {
        id: 3,
        titulo: 'CAPITAO AMERICA',
        classificacao: 10,
        lancamento: false
    },
    {
        id: 4,
        titulo: 'HOMEM DE FERRO',
        classificacao: 8,
        lancamento: false
    },
    {
        id: 1,
        titulo: 'HOMEM ARANHA',
        classificacao: 10,
        lancamento: true
    },
    {
        id: 2,
        titulo: 'THE BATMAN',
        classificacao: 14,
        lancamento: true
    },
    {
        id: 3,
        titulo: 'CAPITAO AMERICA',
        classificacao: 10,
        lancamento: false
    },
    {
        id: 4,
        titulo: 'HOMEM DE FERRO',
        classificacao: 8,
        lancamento: false
    },
    {
        id: 1,
        titulo: 'HOMEM ARANHA',
        classificacao: 10,
        lancamento: true
    },
    {
        id: 2,
        titulo: 'THE BATMAN',
        classificacao: 14,
        lancamento: true
    },
    {
        id: 3,
        titulo: 'CAPITAO AMERICA',
        classificacao: 10,
        lancamento: false
    },
    {
        id: 4,
        titulo: 'HOMEM DE FERRO',
        classificacao: 8,
        lancamento: false
    },
];

const Filmes: React.FC = () => {
    return (
        <>
            <Row justify='start'>
                <Col >
                    <Title>Filmes</Title>
                </Col>
            </Row>
            <Row justify='space-between' align='middle' className='gutter-box'>
                <Col>
                    <Search placeholder="Buscar" enterButton="Pesquisar" />
                </Col>
                <Col>
                    <Row className='actions-buttons'>
                        <Col>
                            <Button type="primary">
                                <ImportOutlined />
                                Importar
                            </Button>
                        </Col>
                        <Col>
                            <FilmeModal />
                        </Col>
                    </Row>
                </Col>
            </Row>
            <Row justify='center' align='middle' className='gutter-box'>
                <Col span={24}>
                    <Table columns={columns} dataSource={data} scroll={{ x: 800, y:450 }} />
                </Col>
            </Row>
        </>
    )
};

export default Filmes;
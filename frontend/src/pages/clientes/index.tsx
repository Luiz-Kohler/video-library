import React, { useEffect, useState } from 'react';
import './index.css'
import { Col, Row, Space, Table, Tag, Tooltip } from 'antd';
import type { ColumnsType } from 'antd/lib/table';
import { ClienteStatus, GetClienteStatusLabel } from '../../enums/cliente'
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import Search from 'antd/lib/input/Search';
import Title from 'antd/lib/typography/Title';
import CriarClienteModal from '../../components/modals/clientes/criar-cliente-modal';
import AtualizarClienteModal from '../../components/modals/clientes/atualizar-cliente-modal';
import { FormataStringData } from '../../utils/dateMethods'
import { ClienteResponse, ExcluirCliente, ListarClientes } from '../../services/clientes/api';
import { toast } from 'react-toastify';

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

const Clientes: React.FC = () => {

    const [atualizarClienteModalVisible, setAtualizarClienteModalVisible] = useState<boolean>(false);
    const [clienteAtualizarId, setClienteAtualizarId] = useState<number>(0);
    const [filtro, setFiltro] = useState<string>();
    const [clientes, setClientes] = useState<ClienteResponse[]>();
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        ListarClientes().then(res => {
            setIsLoading(true);
            setClientes(res.clientes)
            setIsLoading(false);
        })
    }, [isLoading])

    const columns: ColumnsType<ClienteResponse> = [
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
            render: (_, { dataNascimento }) => FormataStringData(dataNascimento)
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
            render: (_, { id }) => (
                <Space size="middle">
                    <Tooltip title="Editar">
                        <EditOutlined onClick={() => {
                            setClienteAtualizarId(id);
                            setAtualizarClienteModalVisible(true)
                        }}
                        />
                    </Tooltip>

                    <Tooltip title="Excluir">
                        <DeleteOutlined
                            className='actions'
                            style={{ color: 'red' }}
                            onClick={() => {
                                ExcluirCliente(id).then(() => {
                                    toast.success(`Cliente com Id: ${id} excluido com sucesso`)
                                    setIsLoading(true);
                                });
                            }}
                        />
                    </Tooltip>
                </Space>
            )
        },
    ];

    return (
        <>
            <AtualizarClienteModal
                isVisible={atualizarClienteModalVisible}
                setVisableFalse={() => setAtualizarClienteModalVisible(false)}
                atualizar={() => setIsLoading(true)}
                id={clienteAtualizarId}
            />
            <Row justify='start'>
                <Col>
                    <Title>Clientes</Title>
                </Col>
            </Row>
            <Row justify='space-between' align='middle' className='gutter-box'>
                <Col>
                    <Search
                        placeholder="Buscar Id, Nome ou CPF"
                        enterButton="Pesquisar"
                        onSearch={(value) => setFiltro(value.toLocaleLowerCase())}
                    />
                </Col>
                <Col>
                    <CriarClienteModal atualizar={() => setIsLoading(true)} />
                </Col>
            </Row>
            <Row justify='center' align='middle' className='gutter-box'>
                <Col span={24}>
                    <Table
                        columns={columns}
                        dataSource={clientes?.filter(cliente => `${cliente.id} ${cliente.nome} ${cliente.cpf}`.toLocaleLowerCase().includes(filtro || ''))}
                        scroll={{ x: 800, y: 450 }}
                        loading={isLoading}
                        size='small'
                    />
                </Col>
            </Row>
        </>
    )
};

export default Clientes;
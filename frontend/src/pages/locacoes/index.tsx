import React, { useEffect, useState } from 'react';
import './index.css'
import { Button, Col, Row, Space, Table, Tag, Tooltip } from 'antd';
import type { ColumnsType } from 'antd/lib/table';
import { LocacaoStatus, GetLocacaoStatusLabel } from '../../enums/locacao'
import { EditOutlined, DeleteOutlined, RetweetOutlined, DownloadOutlined } from '@ant-design/icons';
import { FormataStringData } from '../../utils/dateMethods'
import Search from 'antd/lib/input/Search';
import Title from 'antd/lib/typography/Title';
import CriarLocacaoModal from '../../components/modals/locacoes/criar-locacao-modal';
import AtualizarLocacaoModal from '../../components/modals/locacoes/atualizar-locacao-modal';
import { DevolverFilme, ExcluirLocacao, ListarLocacoes, LocacaoResponse } from '../../services/locacoes/api';
import { toast } from 'react-toastify';
import { BaixarRelatorio } from '../../services/relatorios/api';

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

const Locacoes: React.FC = () => {
    const [atualizarLocacaoModalVisible, setAtualizarLocacaoModalVisible] = useState<boolean>(false);
    const [locacaoAtualizarId, setLocacaoAtualizarId] = useState<number>(0);
    const [filtro, setFiltro] = useState<string>();
    const [locacoes, setLocacoes] = useState<LocacaoResponse[]>();
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        ListarLocacoes().then(res => {
            setIsLoading(true);
            setLocacoes(res.locacoes)
            setIsLoading(false);
        })
    }, [isLoading])

    const columns: ColumnsType<LocacaoResponse> = [
        {
            title: 'Id',
            dataIndex: 'id',
            key: 'id',
        },
        {
            title: 'Cliente',
            dataIndex: 'cliente',
            render: (_, { cliente }) => `${cliente.id} - ${cliente.nome}`
        },
        {
            title: 'Filme',
            dataIndex: 'filme',
            render: (_, { filme }) => `${filme.id} - ${filme.titulo}`

        },
        {
            title: 'Data Locação',
            dataIndex: 'dataLocacao',
            render: (_, { dataLocacao }) => FormataStringData(dataLocacao)
        },
        {
            title: 'Data Prazo Devolução',
            dataIndex: 'dataPrazoDevolucao',
            render: (_, { dataPrazoDevolucao }) => FormataStringData(dataPrazoDevolucao)
        },
        {
            title: 'Data Devolução',
            dataIndex: 'dataDevolucao',
            render: (_, { dataDevolucao }) => dataDevolucao != null ? FormataStringData(dataDevolucao) : ""
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
            render: (_, { id, dataDevolucao }) => (
                <Space size="middle">

                    {dataDevolucao === null &&
                        <Tooltip title="Devolver Filme">
                            <RetweetOutlined className='action' onClick={() => {
                                DevolverFilme(id)
                                    .then(() => {
                                        toast.success(`Locação com Id: ${id} devolvida com sucesso`);
                                        setIsLoading(true);
                                    })
                            }} />
                        </Tooltip>}

                    <Tooltip title="Editar">
                        <EditOutlined className='actions' onClick={() => {
                            setLocacaoAtualizarId(id);
                            setAtualizarLocacaoModalVisible(true)
                        }} />
                    </Tooltip>

                    <Tooltip title="Excluir">
                        <DeleteOutlined
                            className='actions'
                            style={{ color: 'red' }}
                            onClick={() => {
                                ExcluirLocacao(id).then(() => {
                                    toast.success(`Locação com Id: ${id} excluida com sucesso`)
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
            <AtualizarLocacaoModal
                isVisible={atualizarLocacaoModalVisible}
                setVisableFalse={() => setAtualizarLocacaoModalVisible(false)}
                atualizar={() => setIsLoading(true)}
                id={locacaoAtualizarId}
            />
            <Row justify='start'>
                <Col >
                    <Title>Locações</Title>
                </Col>
            </Row>
            <Row justify='space-between' align='middle' className='gutter-box'>
                <Col>
                    <Search
                        placeholder="Buscar por Id"
                        enterButton="Pesquisar"
                        onSearch={(value) => setFiltro(value)}
                        type="number"
                    />
                </Col>
                <Row className='actions-buttons'>
                    <Button 
                        type='primary' 
                        icon={<DownloadOutlined />}
                        onClick={() => BaixarRelatorio()}
                    >
                        Baixar relatório
                    </Button>
                    <Col>
                        <CriarLocacaoModal atualizar={() => setIsLoading(true)} />
                    </Col>
                </Row>
            </Row>
            <Row justify='center' align='middle' className='gutter-box'>
                <Col span={24}>
                    <Table
                        columns={columns}
                        dataSource={locacoes?.filter(locacao => locacao.id.toString().includes(filtro || ""))}
                        scroll={{ x: 800, y: 450 }}
                        loading={isLoading}
                        size='small'
                    />
                </Col>
            </Row>
        </>
    )
};

export default Locacoes;
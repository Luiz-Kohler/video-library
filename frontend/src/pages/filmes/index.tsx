import React, { useEffect, useState } from 'react';
import './index.css'
import { ExcluirFilme, FilmeResponse, ListarFilmes } from '../../services/filmes/api';
import { Col, Row, Space, Table, Tooltip } from 'antd';
import type { ColumnsType } from 'antd/lib/table';
import { CheckOutlined, CloseOutlined, EditOutlined, DeleteOutlined } from '@ant-design/icons';
import Search from 'antd/lib/input/Search';
import Title from 'antd/lib/typography/Title';
import CriarFilmeModal from '../../components/modals/filmes/criar-filme-modal'
import AtualizarFilmeModal from '../../components/modals/filmes/atualizar-filme-modal';
import ImportarFilmesModal from '../../components/modals/filmes/importar-filmes-modal';
import { toast } from 'react-toastify';
import { GetClassificacaoLabel } from '../../enums/filme';

const Filmes: React.FC = () => {
    const [atualizarFilmeModalVisible, setAtualizarFilmeModalVisible] = useState<boolean>(false);
    const [filmeAtualizarId, setFilmeAtualizarId] = useState<number>(0);
    const [filtro, setFiltro] = useState<string>();
    const [filmes, setFilmes] = useState<FilmeResponse[]>();
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        ListarFilmes().then(res => {
            setIsLoading(true);
            setFilmes(res.filmes)
            setIsLoading(false);
        })
    }, [isLoading])

    const columns: ColumnsType<FilmeResponse> = [
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
            render: (_, { classificacao }) => GetClassificacaoLabel(classificacao)
        },
        {
            title: 'Lançamento',
            dataIndex: 'lancamento',
            render: (_, { ehLancamento }) => ehLancamento
                ? <CheckOutlined style={{ color: 'green' }} />
                : <CloseOutlined style={{ color: 'red' }} />
        },
        {
            title: 'Ações',
            dataIndex: 'acao',
            render: (_, { id }) => (
                <Space size="middle">
                    <Tooltip title="Editar">
                        <EditOutlined onClick={() => {
                            setFilmeAtualizarId(id);
                            setAtualizarFilmeModalVisible(true)
                        }}
                        />
                    </Tooltip>

                    <Tooltip title="Excluir">
                        <DeleteOutlined
                            className='actions'
                            style={{ color: 'red' }}
                            onClick={() => {
                                ExcluirFilme(id).then(() => {
                                    toast.success(`Filme com Id: ${id} excluido com sucesso`)
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
            <AtualizarFilmeModal
                isVisible={atualizarFilmeModalVisible}
                setVisableFalse={() => setAtualizarFilmeModalVisible(false)}
                atualizar={() => setIsLoading(true)}
                id={filmeAtualizarId}
            />
            <Row justify='start'>
                <Col >
                    <Title>Filmes</Title>
                </Col>
            </Row>
            <Row justify='space-between' align='middle' className='gutter-box'>
                <Col>
                    <Search
                        placeholder="Buscar por Id ou Titulo"
                        enterButton="Pesquisar"
                        onSearch={(value) => setFiltro(value.toLocaleLowerCase())}
                    />
                </Col>
                <Col>
                    <Row className='actions-buttons'>
                        <Col>
                            <ImportarFilmesModal atualizar={() => setIsLoading(true)} />
                        </Col>
                        <Col>
                            <CriarFilmeModal atualizar={() => setIsLoading(true)} />
                        </Col>
                    </Row>
                </Col>
            </Row>
            <Row justify='center' align='middle' className='gutter-box'>
                <Col span={24}>
                    <Table
                        columns={columns}
                        dataSource={filmes?.filter(filme => `${filme.id} - ${filme.titulo}`.toLocaleLowerCase().includes(filtro || ''))}
                        scroll={{ x: 800, y: 450 }}
                        loading={isLoading}
                        size='small'
                    />
                </Col>
            </Row>
        </>
    )
};

export default Filmes;
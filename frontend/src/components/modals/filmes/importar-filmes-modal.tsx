import React, { useState } from 'react';
import { ImportOutlined } from '@ant-design/icons';
import { Button, Input, Modal } from 'antd';
import { toast } from 'react-toastify';
import { ImportarFilmes } from '../../../services/filmes/api';

type ImportarFilmeModalProps = {
    atualizar: any
}

const ImportFilmesModal: React.FC<ImportarFilmeModalProps> = ({ atualizar }) => {
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [arquivo, setArquivo] = useState<File | null>(null);
    const [isLoading, setIsLoading] = useState(false);

    const showModal = () => {
        setIsModalVisible(true);
    };

    const handleOk = () => {
        !!arquivo && ImportarFilmes(arquivo)
            .then((res) => {
                setIsLoading(true);
                if (res.status === 200) {
                    toast.success('Importação concluida.');
                    setIsModalVisible(false);
                    atualizar();
                }
                setIsLoading(false);
            })
    };

    const handleCancel = () => {
        setIsModalVisible(false);
    };

    return (
        <>
            <Button type="primary" onClick={showModal} icon={<ImportOutlined />}>
                Importar
            </Button>
            <Modal destroyOnClose title="Cadastro de Filme" visible={isModalVisible} onOk={handleOk} onCancel={handleCancel} footer={[
                <Button key="back" onClick={handleCancel}  >
                    Voltar
                </Button>,
                <Button
                    key="Ok"
                    type="primary"
                    style={{ marginTop: 16 }}
                    onClick={handleOk}
                    loading={isLoading}
                >
                    Começar importação
                </Button>
            ]}>
                <Input type='file' accept='.csv' onChange={(e) => {
                    if (e.target.files != null) {
                        setArquivo(e.target.files[0])
                    }
                }}/>
            </Modal>
        </>
    );
};

export default ImportFilmesModal;
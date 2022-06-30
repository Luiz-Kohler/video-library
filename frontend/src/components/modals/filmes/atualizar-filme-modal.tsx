import React, { useEffect, useState } from 'react';
import { Button, Checkbox, Form, Input, Modal, Select } from 'antd';
import { AtualizarFilme, AtualizarFilmeRequest, BuscarFilme } from '../../../services/filmes/api';
import { toast } from 'react-toastify';
import { Classificacao } from '../../../enums/filme';
const { Option } = Select;

type AtualizarFilmeModalProps = {
    isVisible: boolean;
    setVisableFalse: any;
    id: number;
    atualizar: any
}
const AtualizarFilmeModal: React.FC<AtualizarFilmeModalProps> = ({ isVisible, setVisableFalse, atualizar, id }) => {
    const [form] = Form.useForm();

    const [filme, setFilme] = useState<AtualizarFilmeRequest>({
        id: 0,
        classificacao: Classificacao.Livre,
        titulo: "",
        ehLancamento: false
    });

    useEffect(() => {
        if (id !== 0) {
            BuscarFilme(id).then(res => {
                setFilme({ id: id, titulo: res.titulo, classificacao: res.classificacao, ehLancamento: res.ehLancamento })
                form.setFieldsValue({ id: id, titulo: res.titulo, classificacao: res.classificacao, ehLancamento: res.ehLancamento })
            })
        }
    }, [id])

    const handleOk = () => {
        AtualizarFilme(filme).then((res) => {
            if (res.status === 200) {
                setFilme({ id: 0, titulo: "", classificacao: Classificacao.Livre, ehLancamento: false })
                setVisableFalse(false);
                atualizar();
                toast.success("Filme atualizado com sucesso!")
            }
        })
    };

    const handleCancel = () => {
        setVisableFalse();
    };

    return (
        <>
            <Modal title="Atualização de Filme" visible={isVisible} onOk={form.submit} onCancel={handleCancel} footer={[
                <Button key="back" onClick={handleCancel}>
                    Voltar
                </Button>,
                <Button type='primary' form="atualizar-filme-form" key="submit" htmlType="submit">
                    Atualizar
                </Button>
            ]}>
                <Form
                    id="atualizar-filme-form"
                    form={form}
                    layout="vertical"
                    labelCol={{ span: 8 }}
                    wrapperCol={{ span: 16 }}
                    onFinish={handleOk}
                    autoComplete="off"
                >
                    <Form.Item label="Id" name="id" rules={[{ required: true, message: 'Informe o Id' }]}>
                        <Input type={"number"} placeholder="Id" value={filme?.id} onChange={(e) => setFilme({ ...filme, id: parseInt(e.target.value) })} />
                    </Form.Item>
                    <Form.Item label="Titulo" name="titulo" rules={[{ required: true, message: 'Informe o Titulo' }]}>
                        <Input placeholder="Titulo" value={filme?.titulo} onChange={(e) => setFilme({ ...filme, titulo: e.target.value })} />
                    </Form.Item>
                    <Form.Item label="Classificação" name="classificacao" rules={[{ required: true, message: 'Informe classificação' }]}>
                    <Select style={{ width: 120 }} value={filme.classificacao.toString()} onChange={(value) => setFilme({ ...filme, classificacao: parseInt(value) })}>
                            <Option value="0">Livre</Option>
                            <Option value="10">Dez anos</Option>
                            <Option value="12">Doze anos</Option>
                            <Option value="14">Catorze anos</Option>
                            <Option value="16">Dezesseis anos</Option>
                            <Option value="18">Dezoito anos</Option>
                        </Select>
                    </Form.Item>
                    <Form.Item label="Lançamento">
                        <Checkbox checked={filme?.ehLancamento} onChange={(e) => setFilme({ ...filme, ehLancamento: e.target.checked })} />
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

export default AtualizarFilmeModal;
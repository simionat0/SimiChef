function valida_form_cli(frm) {
    
    retorno = true;
    
    if (document.getElementById("nome_completo").value === "") {
        alert('Por favor, preencha o Nome');
        document.getElementById("nome_completo").focus();
        retorno = false;
    }
    if (document.getElementById("cep").value === "") {
        alert('Por favor, preencha o CEP');
        document.getElementById("cep").focus();
        retorno = false;
    }
    if (document.getElementById("uf").value === "") {
        alert('Por favor, preencha o Estado');
        document.getElementById("uf").focus();
        retorno = false;
    }
    if (document.getElementById("numero").value === "") {
        alert('Por favor, preencha o Numero');
        document.getElementById("numero").focus();
        retorno = false;
    }
    if (document.getElementById("logradouro").value === "") {
        alert('Por favor, preencha o Logradouro');
        document.getElementById("logradouro").focus();
        retorno = false;
    }
    if (document.getElementById("cidade").value === "") {
        alert('Por favor, preencha o Cidade');
        document.getElementById("cidade").focus();
        retorno = false;
    }
    if (document.getElementById("bairro").value === "") {
        alert('Por favor, preencha o Bairro');
        document.getElementById("bairro").focus();
        retorno = false;
    }
    if (document.getElementById("email").value === "") {
        alert('Por favor, preencha o E-mail');
        document.getElementById("email").focus();
        retorno = false;
    }
    if (document.getElementById("telefone").value === "") {
        alert('Por favor, preencha o Telefone');
        document.getElementById("telefone").focus();
        retorno = false;
    }
    if (document.getElementById("senha").value === "") {
        alert('Por favor, preencha o Senha');
        document.getElementById("senha").focus();
        retorno = false;
    }
    
     if (retorno) {
        document.getElementById(frm).submit();
    }

}
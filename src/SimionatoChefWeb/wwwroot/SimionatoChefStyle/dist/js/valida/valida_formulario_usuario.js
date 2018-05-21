function valida_form_user(frm) {
    retorno = true;
    if (document.getElementById("nome").value === "") {
        alert('Por favor, preencha o nome');
        document.getElementById("nome").focus();
        retorno = false;
    }
    if (document.getElementById("cargo").value === "") {
        alert('Por favor, preencha o cargo do usuário');
        document.getElementById("cargo").focus();
        retorno = false;
    }
    if (document.getElementById("telefone").value === "") {
        alert('Por favor, preencha o telefone do usuário');
        document.getElementById("telefone").focus();
        retorno = false;
    }
    if (document.getElementById("email").value === "") {
        alert('Por favor, preencha o e-mail do usuário');
        document.getElementById("email").focus();
        retorno = false;
    }
    if (document.getElementById("senha").value === "") {
        alert('Por favor, preencha a senha do usuário');
        document.getElementById("senha").focus();
        retorno = false;
    }
    if (retorno) {
        document.getElementById(frm).submit();
    }
}
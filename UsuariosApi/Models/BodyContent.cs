namespace UsuariosApi.Models;

public class BodyContent
{
    public string Value { get; set; }

    public BodyContent(int usuarioId, string codigo)
    {
        Value = $"http://localhost:6000/ativa?UsuarioId={usuarioId}&CodigoDeAtivacao={codigo}";
    }
}

using System.Diagnostics;

namespace Jitsukawa.Extensions.Shell
{
    public static class Shell
    {
        /// <summary>
        /// Executa um comando no terminal do sistema e captura sua saída.
        /// </summary>
        /// <param name="directory">Diretório no qual o comando deve executar.</param>
        /// <param name="parameters">Parâmetros do comando ou aplicativo invocado.</param>
        /// <param name="output">Resultado que seria exibido no terminal do sistema.</param>
        /// <returns>Código de retorno do comando ou aplicativo executado.</returns>
        public static int Run(this string command, string directory, string parameters, out string output)
        {
            int retorno = -1;

            var argumentos = new ProcessStartInfo(command, parameters);
            argumentos.CreateNoWindow = true;
            argumentos.UseShellExecute = false;
            argumentos.RedirectStandardOutput = true;

            if (!string.IsNullOrEmpty(directory))
                argumentos.WorkingDirectory = directory;

            using (Process processo = new Process())
            {
                processo.StartInfo = argumentos;
                processo.Start();
                output = processo.StandardOutput.ReadToEnd();
                processo.WaitForExit();
                retorno = processo.ExitCode;
            }

            return retorno;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Dtos;

namespace Types.Layer.Contracts.Interfaces
{
    public interface IMailOrchestration
    {
        bool SendMail(MailDto mailDto);
    }
}

<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="FM_InvoiceDetail.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_InvoiceDetail"
    Title="发票明细信息" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
  <script type="text/javascript" language="javascript">
    
  
     function amountcheck(obj) 
    {        
        Num=obj.value;
        newchar = ""; 
        if(Num=="")
        {
           alert("金额不能为空!");
           obj.focus();
           return newchar;
        }
        for(i=Num.length-1;i>=0;i--)
        {
          Num = Num.replace(",","")//替换tomoney()中的“,”
        }
       if(isNaN(Num)) 
       { //验证输入的字符是否为数字
         alert("请检查小写金额是否正确!");
         obj.focus();
         return newchar;
       }
    }
    
    //含税单价变化 修改单价，金额，税额，含税金额
    function amountcheckhsdj(obj) 
    {        
       
            var table=document.getElementById("ctl00_PrimaryContent_grvInvDetail");
            var tr=table.getElementsByTagName("tr");           
            var wlsl=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;    
            var hsdj=obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
            var sl=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
            var dj=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            var hjje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;      
           
            if(hsdj!=null)
            {
              //单价
              dj=(hsdj/(1+sl/100)).toFixed(4);
              obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=dj;
             //金额
              je=(hsdj/(1+sl/100)*wlsl).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=je;
              //税额
              hjje=(wlsl*hsdj).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=hjje;
              
              
              se=(hjje-je).toFixed(2);      
              obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=se;
              //含税金额

            }
            
            Statistic();
    }
      //税率变化 修改单价，金额，税额，含税金额
    function amountchecksl(obj) 
    {        
       
            var table=document.getElementById("ctl00_PrimaryContent_grvInvDetail");
            var tr=table.getElementsByTagName("tr");           
            var wlsl=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;  //数量  
            var hsdj=obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;//含税单价
            var sl=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;//税率
            var dj=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;//单价
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;//金额
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;//税额
            var hjje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;  //含税金额    
           
            if(sl!=null)
            {
              //单价
              dj=(hsdj/(1+sl/100)).toFixed(4);
              obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=dj;
             //金额
              je=(hsdj/(1+sl/100)*wlsl).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=je;
              //税额
              hjje=(wlsl*hsdj).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=hjje;
              
              
              se=(hjje-je).toFixed(2);      
              obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=se;
              //含税金额

            }
            
            Statistic();
    }
    
    //含税金额变化修改含税单价，单价，金额，税额
     function amountcheckhjje(obj) 
    {        
       
            var table=document.getElementById("ctl00_PrimaryContent_grvInvDetail");
            var tr=table.getElementsByTagName("tr");           
            var wlsl=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;    
            var hsdj=obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
            var sl=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
            var dj=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            var hjje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;        
           
            if(hjje!=null)
            {
              //含税单价
              hsdj=(hjje/wlsl).toFixed(4);
              obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=hsdj;
              
              //单价
              dj=(hsdj/(1+sl/100)).toFixed(4);
              obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=dj;
              
              //金额
              je=(dj*wlsl).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=je;
                         
              //税额
              se=(hjje-je).toFixed(2);         
              obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=se;
            }
            
            Statistic();
            
    }
    
    //单价变化修改 含税单价，含税金额，金额，税额
     function amountcheckdj(obj) 
    {        
       
            var table=document.getElementById("ctl00_PrimaryContent_grvInvDetail");
            var tr=table.getElementsByTagName("tr");           
            var wlsl=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;    
            var hsdj=obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
            var sl=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
            var dj=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            var hjje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;        
           
            if(dj!=null)
            {
            
              //含税单价
              hsdj=(dj*(1+sl/100)).toFixed(4);
              obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=hsdj;
              
              //金额
              je=(dj*wlsl).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=je;
               
               //含税金额
              hjje=(dj*(1+sl/100)*wlsl).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=hjje;
              
                //税额          
              se=(hjje-je).toFixed(2);         
              obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=se;
            }
            
            Statistic();
    }
    
    //金额变化修改 含税单价，单价，含税金额，税额
    function amountcheckje(obj) 
    {        
       
            var table=document.getElementById("ctl00_PrimaryContent_grvInvDetail");
            var tr=table.getElementsByTagName("tr");       
            //数量    
            var wlsl=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value; 
            //含税单价  
            var hsdj=obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
            //税率
            var sl=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
            //单价
            var dj=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
            //金额
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            //税额
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            //含税金额
            var hjje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;        
           
            if(je!=null)
            {
            
             //含税单价
              hsdj=(je/wlsl*(1+sl/100)).toFixed(4);
              obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=hsdj;
              
              //单价
              dj=(je/wlsl).toFixed(4);
              obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=dj;
              
              //含税金额
              hjje=(dj*(1+sl/100)*wlsl).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=hjje;
              
                //税额           
              se=(hjje-je).toFixed(2);         
              obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=se;
             
            }
            
            Statistic();
    }
    
     //税额变化修改 含税单价，单价，含税金额，税额
    function amountcheckse(obj) 
    {        
       
            var table=document.getElementById("ctl00_PrimaryContent_grvInvDetail");
            
            var tr=table.getElementsByTagName("tr");       
            //数量    
            var wlsl=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value; 
            //含税单价  
            var hsdj=obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
            //税率
            var sl=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
            //单价
            var dj=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
            //金额
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            //税额
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            //含税金额
            var hjje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;        
           
            if(se!=null)
            {
              hjje=(parseFloat(je)+parseFloat(se)); 
              obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=hjje.toFixed(2);

            }
            
            Statistic();
    }
    
    
    
     /*数据统计函数*/
        function Statistic() {
            var je = 0;
            var se = 0;
            var hsje = 0;
            var gv1 = document.getElementById("<%=grvInvDetail.ClientID %>");
            for (i = 1; i < (gv1.rows.length - 1); i++)
            {
                var val1 = gv1.rows[i].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
                je += parseFloat(val1);
                var val2 = gv1.rows[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
                se += parseFloat(val2);
                var val3 = gv1.rows[i].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;
                hsje += parseFloat(val3);
            }
            var lbtn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[10].getElementsByTagName("span")[0];
            lbtn.innerHTML = je.toFixed(2);
            var lbtq = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[11].getElementsByTagName("span")[0];
            lbtq.innerHTML = se.toFixed(2);
            var lbta = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[12].getElementsByTagName("span")[0];
            lbta.innerHTML = hsje.toFixed(2);
        }
</script>




    <div style="width: 950px; height: 450px; margin-right: auto; margin-left: auto; overflow: auto;">
    <div style="width: 900px" align="right"><asp:Button ID="btnsave" runat="server" 
            Text="修改" onclick="btnsave_Click" /></div>
        <asp:HiddenField ID="hfdfp" runat="server" />
        <asp:GridView ID="grvInvDetail" Width="98%" CssClass="toptable grid" runat="server"
            DataKeyNames="GI_UNICODE" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="True" OnRowDataBound="grvInvDetail_RowDataBound">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField HeaderText="唯一值" DataField="GI_UNICODE" ReadOnly="true" Visible="false" ItemStyle-HorizontalAlign="Center">
                 </asp:BoundField>
                <asp:TemplateField HeaderText="入库单号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbfp" runat="server" Text='<%#Eval("WG_CODE").ToString()%>'> </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbmid" runat="server" Text='<%#Eval("GI_MATCODE").ToString()%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="物料名称" DataField="GI_NAME" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
                </asp:BoundField>
                <asp:BoundField HeaderText="规格" DataField="GI_GUIGE" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
                </asp:BoundField>
                <asp:BoundField HeaderText="单位" DataField="GI_UNIT" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
                </asp:BoundField>
                <asp:TemplateField HeaderText="数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="text_sj" runat="server" ReadOnly="true" Text='<%#Eval("GI_NUM") %>'
                            BorderStyle="None" Width="80px" onblur="amountcheck(this)"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="含税单价" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtGI_TADJ" runat="server" Text='<%#Eval("GI_UNITPRICE")%>' BorderStyle="None"
                            Width="80px" onblur="amountcheckhsdj(this)"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="税率(%)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtGI_TAXRATE" runat="server" Text='<%#Eval("GI_TAXRATE")%>' BorderStyle="None"
                            Width="80px" onblur="amountcheck(this)" onchange="amountchecksl(this)"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="单价" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtGI_dj" runat="server" Text='<%#Eval("GI_CTAXUPRICE")%>' BorderStyle="None"
                            Width='80px' onchange="amountcheck(this)" onblur="amountcheckdj(this)"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lbheji" runat="server" Text="合计"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="金额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtGI_je" runat="server" Text='<%#Eval("GI_AMTMNY") %>' BorderStyle="None"
                            Width='80px' onchange="amountcheck(this)" onblur="amountcheckje(this)"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lbje" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="税额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtGI_se" runat="server" Text='<%#Eval("GI_SE") %>' BorderStyle="None"
                            Width='80px' onchange="amountcheck(this)" onblur="amountcheckse(this)"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lbse" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="含税金额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtGI_HJPRICE" runat="server" Text='<%#Eval("GI_CTAMTMNY") %>' BorderStyle="None"
                            Width='80px' onchange="amountcheck(this)"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lbhsje" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
        </asp:GridView>
    </div>
</asp:Content>

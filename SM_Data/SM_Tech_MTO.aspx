<%@ Page Title="技术MTO调整" Language="C#" MasterPageFile="~/Masters/BaseMaster.master"
    AutoEventWireup="true" CodeBehind="SM_Tech_MTO.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Tech_MTO" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

    <script language="javascript" type="text/javascript">
     function ShowStoUseModal(ptc,pid) {
            var date=new Date();
            var time=date.getTime();
            var retVal = window.showModalDialog("SM_Tech_MTO_STO.aspx?ptc="+ptc+"&&pid="+pid+"&&id="+time, "", "dialogWidth=1100px;dialogHeight=500px;status=no;help=no;scroll=yes");
            if(retVal!=null)
            {
               if(retVal)
               {
                 document.getElementById("<%=btnSearch.ClientID%>").click();
               }
            }
        }
         function ShowMTOModal(mto) {
            var date=new Date();
            var time=date.getTime();
            var retVal =  window.open("SM_Warehouse_MTOAdjust.aspx?FLAG=OPEN&&action=BG&&ID="+mto);
            if(retVal!=null)
            {
               if(retVal)
               {
                 document.getElementById("<%=btnSearch.ClientID%>").click();
               }
            }
        }
        
        
        function CheckNum(textvalue)
        {
          if(textvalue.value.length!=0)
          {
              var num=parseInt(textvalue.value);
               if(isNaN(num)||num<=0)
               {
                 alert("请输入正确数字！");
               }
               else{textvalue.value=num;}
               
           }
           
        }
        
        
        
  function allsel()
  {
   var  table=document.getElementById('<%= GridView1.ClientID %>');
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length-1;i++)
    {
     if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
       {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
        tr[i].style.backgroundColor = "#adadad";
       }
    }
  }

function cancelsel()
{
   var  table=document.getElementById('<%= GridView1.ClientID %>');
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length-1;i++)
    {
       if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
       {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
        tr[i].style.backgroundColor = "#EFF3FB";
       }
    }
}

function consel()
{
    table=document.getElementById('<%= GridView1.ClientID %>');
    tr=table.getElementsByTagName("tr");//这里的tr是一个数组
    for(var i=1;i<(tr.length-1);i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
        if(obj.type.toLowerCase()=="checkbox" && obj.value!="")
        {
            if(obj.checked)
            {
                obj.checked=true;

                for(var j=i+1;j<tr.length-1;j++)
                {
                    var nextobj=tr[j].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    if(nextobj!=null)
                    {
                    
                        if(nextobj.type.toLowerCase()=="checkbox" && nextobj.value!="")
                        {
                            if(nextobj.checked)
                            {
                                for(var k=i+1;k<j+1;k++)
                                {
                                    tr[k].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                    tr[k].style.backgroundColor = "#adadad";
                                }
                            }
                        }  
                    }
                }
            }
        }
    }
}
        
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="center" width="30%">
                            <asp:CheckBox ID="CheckBoxMy" runat="server" Text="我的任务" 
                                oncheckedchanged="CheckBoxMy_CheckedChanged" AutoPostBack="true"  Checked="true"/>
                            
                        </td>
                        <td align="left" width="40%">
                            <asp:RadioButtonList ID="RadioButtonListState" runat="server" AutoPostBack="True"
                                RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonListState_SelectedIndexChanged">
                                <asp:ListItem Value="1" Selected="True">未调整</asp:ListItem>
                                <asp:ListItem Value="2">调整中</asp:ListItem>
                                <asp:ListItem Value="3">已调整</asp:ListItem>
                                <asp:ListItem Value="4">关闭</asp:ListItem>
                                <asp:ListItem Value="0">全部</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right" width="30%">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查询" OnClick="ButtonSearch_Click" />
                            <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" Style="display: none" />
                            &nbsp;
                            <asp:Button ID="reSetCondition" runat="server" Text="重置" OnClick="reSetCondition_Click" />
                            <asp:Button ID="ButtonAntiAdjust" runat="server" Text="反调整" OnClick="ButtonAntiAdjust_Click" />
                            &nbsp;
                            <asp:Button ID="ButtonClose" runat="server" Text="反关闭" OnClick="ButtonAntiClose_Click" />
                            &nbsp;
                            <asp:Button ID="ButtonMTO" runat="server" Text="MTO" OnClick="ButtonOperate_Click" />
                            &nbsp;
                            <asp:Button ID="ButtonDaoChu" runat="server" Text="导出" OnClick="ButtonDaoChu_Click" />
                            
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:Panel ID="OperationPanel" runat="server" Width="100%" Visible="true" ScrollBars="Auto">
                <table width="100%">
                    <tr>
                        <td width="25%">
                            &nbsp;项目名称:<asp:TextBox ID="TextBoxPro" runat="server" onclick="this.select();"></asp:TextBox>
                        </td>
                        <td width="25%">
                            工程名称:<asp:TextBox ID="TextBoxEng" runat="server" onclick="this.select();"></asp:TextBox>
                        </td>
                        <td width="25%">
                            &nbsp;&nbsp;&nbsp;技术员:<asp:TextBox ID="TextBoxTec" runat="server" onclick="this.select();"></asp:TextBox>
                        </td>
                        <td width="25%">
                            计划跟踪号:<asp:TextBox ID="TextBoxPTC" runat="server" onclick="this.select();"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td width="25%">
                            &nbsp;物料编码:<asp:TextBox ID="TextBoxMar" runat="server" onclick="this.select();"></asp:TextBox>
                        </td>
                        <td width="25%">
                         物料名称:<asp:TextBox ID="TextBoxMarNM" runat="server" onclick="this.select();"></asp:TextBox>
                        </td>
                        <td width="25%">
                        规格型号:<asp:TextBox ID="TextBoxGG" runat="server" onclick="this.select();"></asp:TextBox>
                        </td>
                        <td width="25%">
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 材质:<asp:TextBox ID="TextBoxCZ" runat="server" onclick="this.select();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            &nbsp;&nbsp;&nbsp;&nbsp;执行人:<asp:TextBox ID="TextBoxEXEC" runat="server" onclick="this.select();"></asp:TextBox>
                        </td>
                        <td width="25%">
                            显示条数:<asp:TextBox runat="server" ID="TextBoxnum" onblur="CheckNum(this)"></asp:TextBox>
                        </td>
                        <td width="25%">
                        </td>
                        <td width="25%">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
             <div>
                &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;
                <input id="continue" type="button" value="连选" onclick="consel()" />
                &nbsp;&nbsp;&nbsp;<input id="cancelsel1" type="button" value="取消" onclick="cancelsel()" />
            </div>
            <asp:Panel ID="PanelBody1" runat="server" Style="position: static; overflow: auto;"
                Width="100%" Height="350px">
                <asp:GridView ID="GridView1" runat="server" EmptyDataText="无数据!" OnRowDataBound="GridView1_RowDataBound"
                    AutoGenerateColumns="false" DataKeyNames="MP_ID,MP_CHPTCODE,MP_EXECSTATE" ShowFooter="true">                                       
                    <Columns>
                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Enabled='<%#string.IsNullOrEmpty(Eval("MTO").ToString())%>' /><%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MP_CHPCODE" HeaderText="变更批号" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="MP_PROJNAME" HeaderText="项目名称" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="MP_ENGNM" HeaderText="工程名称" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="MP_CHPTCODE" HeaderText="计划跟踪号" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="MP_MARID" HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="MNAME" HeaderText="物料名称" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="GUIGE" HeaderText="规格型号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="CAIZHI" HeaderText="材质" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="PURCUNIT" HeaderText="单位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="MP_BGNUM" HeaderText="变更数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"  FooterStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="MP_BKNUM" HeaderText="调整数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" FooterStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center">                          
                            </asp:BoundField>
                        <asp:BoundField DataField="MP_SUBNAME" HeaderText="提交人" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="MP_SUBTIME" HeaderText="提交日期" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="BianGtime" HeaderText="变更日期" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:TemplateField HeaderText="MTO" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="LabelMTO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MTO")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MP_EXECNAME" HeaderText="执行人" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:TemplateField HeaderText="执行状态" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="LabelMTOState" runat="server" Text='<%#Eval("MP_EXECSTATE").ToString()=="1"?"未调整":Eval("MP_EXECSTATE").ToString()=="2"?"调整中":Eval("MP_EXECSTATE").ToString()=="3"?"已调整":"关闭"%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MP_EXECSTATE" HeaderText="执行状态" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" Visible="false" ></asp:BoundField>
                        <asp:BoundField DataField="MP_BKNOTE" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" HorizontalAlign="Center"/>
                    <RowStyle BackColor="#EFF3FB" />
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server" ForeColor="Red">
                    没有任务!</asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </asp:Panel>
            
    <script type="text/javascript">  
   var sDataTable=document.getElementById("<%=GridView1.ClientID %>")  
  
   function RowClick()//点击某一行选中
   {
           for (var i=1, j=sDataTable.tBodies[0].rows.length-1; i<j; i++) 
          {
            sDataTable.tBodies[0].rows[i].onclick= function (i) 
            {
                
                var dataRow = sDataTable.tBodies[0].rows[i];
                
                return function () 
                      {
                            if (dataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked) 
                            {
                                dataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                dataRow.style.backgroundColor = "#EFF3FB";
                               
                            }
                            else 
                            {
                                dataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                dataRow.style.backgroundColor = "#adadad";
                               
//                              var odataRow;
//                                  for (var m=1, n=sDataTable.tBodies[0].rows.length-1; m<n; m++) 
//                                  {
//                                     odataRow=sDataTable.tBodies[0].rows[m];
//                                     if(odataRow!=dataRow)
//                                     {
//                                        odataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
//                                        odataRow.style.backgroundColor = "#EFF3FB";
//                                     }
//                                  }
                            }
                        }
             }.call(this, i);
           }
   }

   RowClick();

  </script>
            
        </div>
        <!--box-outer END -->
    </div>
    <!--box-wrapper END -->
</asp:Content>

using CMS;
using CMS.EventLog;
using CMS.OnlineForms;
using Launchpad.Core.Constants;
using Launchpad.Infrastructure.Kentico.Web.Modules;
using Launchpad.Infrastructure.Modules;

[assembly: RegisterModule(typeof(HoneypotModule))]
namespace Launchpad.Infrastructure.Kentico.Web.Modules
{
    public class HoneypotModule : CustomCmsModule
    {
        #region Fields
        #endregion

        public HoneypotModule()
            : base(nameof(HoneypotModule))
        {
            this.SettingDisableModuleCodeName = "DisableHoneypotModule";
        }

        protected override void RegisterModuleEvents()
        {
            BizFormItemEvents.Insert.Before += FormItem_InsertBeforeHandler;
            BizFormItemEvents.Insert.After += FormItem_InsertAfterHandler;

        }

        private void FormItem_InsertBeforeHandler(object sender, BizFormItemEventArgs e)
        {
            BizFormItem formDataItem = e.Item;


            if (formDataItem != null)
            {
                string honeypotFieldValue = formDataItem.GetStringValue(HoneypotConstants.DefaultHoneypotFieldId, string.Empty);

                if (!string.IsNullOrEmpty(honeypotFieldValue))
                {
                    // we don't cancel as it will make the form submission fail					
                    e.Cancel();
                    return;
                }

                var formFields = formDataItem.BizFormInfo.Form.GetFields(true, false);
                bool allValuesEmpty = true;
                foreach (var item in formFields)
                {
                    var itemValue = formDataItem.GetValue(item.ToString());

                    if (itemValue != null)
                    {
                        allValuesEmpty = false;
                    }
                }

                if (allValuesEmpty)
                {
                    e.Cancel();
                }


            }
        }

        private void FormItem_InsertAfterHandler(object sender, BizFormItemEventArgs e)
        {
            BizFormItem formDataItem = e.Item;
            if (formDataItem != null)
            {
                string honeypotFieldValue = formDataItem.GetStringValue(HoneypotConstants.DefaultHoneypotFieldId, string.Empty);
                if (!string.IsNullOrEmpty(honeypotFieldValue))
                {
                    // instead we delete it after its been inserted
                    formDataItem.Delete();
                }
            }

        }
    }

}

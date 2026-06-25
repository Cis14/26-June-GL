define("WorkflowAgentSchemaManagerItem", ["ext-base", "terrasoft", "WorkflowAgentSchema", "CopilotAgentSchema",
		"WorkflowAgentSchemaRequest", "WorkflowAgentSchemaResponse", "CopilotAgentSchemaRequest",
		"CopilotAgentSchemaResponse", "CopilotAgentSchemaCreateRequest", "CopilotAgentSchemaUpdateRequest"
	], function(Ext, Terrasoft) {

		/**
		 * Dcm schema manager class.
		 */
		Ext.define("Terrasoft.manager.WorkflowAgentSchemaManagerItem", {
			extend: "Terrasoft.manager.ProcessSchemaManagerItem",
			alternateClassName: "Terrasoft.WorkflowAgentSchemaManagerItem",

			/**
			 * @inheritdoc Terrasoft.ProcessSchemaManagerItem#instanceClassName
			 * @overridden
			 */
			instanceClassName: "Terrasoft.WorkflowAgentSchema",

			/**
			 * @inheritdoc Terrasoft.ProcessSchemaManagerItem#requestClassName
			 * @overridden
			 */
			requestClassName: "Terrasoft.WorkflowAgentSchemaRequest",

			/**
			 * @private
			 */
			_workflowAgentName: null,

			/**
			 * @private
			 */
			_workflowAgentCaption: null,

			/**
			 * @private
			 */
			_initialStatus: null,

			/**
			 * @private
			 */
			_mergeAgentToProcessSchema: function(processSchema, agentSchema) {
				processSchema.agent = agentSchema;
				return processSchema;
			},

			/**
			 * @inheritdoc Terrasoft.BaseSchemaManagerItem#forceGetInstanceByConfig
			 * @override
			 */
			forceGetInstanceByConfig: function(config, callback, scope) {
				const agentSchemaQuery = Ext.create("Terrasoft.CopilotAgentSchemaRequest", {
					uId: this.uId,
					packageUId: this.packageUId,
				});
				agentSchemaQuery.execute(function(agentSchemaResponse) {
					if (!agentSchemaResponse.success) {
						callback.call(scope, null, agentSchemaResponse);
						return;
					}
					const agentSchema = agentSchemaResponse.schema;
					const workflow = agentSchema.workflow;

					const query = this.getInstanceRequest({
						uId: workflow.workflowSchemaUId,
						packageUId: this.packageUId,
					});
					query.execute(function(processSchemaResponse) {
						if (!processSchemaResponse.success) {
							callback.call(scope, null, processSchemaResponse);
							return;
						}
						const processSchema = processSchemaResponse.schema;
						const resultingSchema = this._mergeAgentToProcessSchema(processSchema, agentSchema);
						callback.call(scope, resultingSchema, processSchemaResponse);
					}, this);

				}, this);
			},

			/**
			 * @private
			 */
			_addAgentSchema: function(schema, schemaConfig) {
				schema.agent = Ext.create("Terrasoft.CopilotAgentSchema", {
					uId: Terrasoft.generateGUID(),
					name: schemaConfig.name,
					caption: schemaConfig.caption,
					packageUId: this.packageUId,
					type: 'WorkflowAgent',
					workflow: {
						workflowSchemaUId: schema.uId,
					}
				});
				return schema;
			},

			/**
			 * @inheritdoc Terrasoft.BaseSchemaManagerItem#createInstance
			 * @override
			 */
			createInstance: function(schemaConfig) {
				this._workflowAgentName = schemaConfig.name;
				this._workflowAgentCaption = schemaConfig.caption;
				const config = Ext.apply({}, {
					name: this._workflowAgentName + '_Process',
					/// TODO: localize caption
					caption: this._workflowAgentCaption + ' Process',
				}, schemaConfig);
				const instance = this.callParent([config]);
				return this._addAgentSchema(instance, schemaConfig);
			},

			/**
			 * @private
			 */
			_saveAgentSchema: function(isNew, callback, scope) {
				const agentSchema = this.instance.agent;
				const schemaRequest = isNew
					? Ext.create("Terrasoft.CopilotAgentSchemaCreateRequest", {
						schemaUId: agentSchema.uId,
						packageUId: this.packageUId,
					})
					: Ext.create("Terrasoft.CopilotAgentSchemaRequest", {
						uId: agentSchema.uId,
						packageUId: this.packageUId,
					});
				const chainFunctions = [
					schemaRequest.execute.bind(schemaRequest),
					function(next, schemaResponse) {
						if (!schemaResponse.success) {
							callback.call(scope, schemaResponse);
							return;
						}
						next(schemaResponse.schema);
					},
					function(next, schema) {
						const mergedSchema = schema.applyChanges(agentSchema);
						next(mergedSchema);
					},
					function(next, mergedSchema) {
						const updateRequest = Ext.create("Terrasoft.CopilotAgentSchemaUpdateRequest", {
							schema: mergedSchema,
						});
						updateRequest.execute(function(response) {
							next(response, mergedSchema);
						}, this);
					},
					function(_, response, mergedSchema) {
						if (response.success) {
							this.instance.agent = mergedSchema;
						}
						callback.call(scope, null);
					},
					this
				];
				Terrasoft.chain.apply(this, chainFunctions);
			},

			/**
			 * @inheritdoc Terrasoft.BaseSchemaManagerItem#setStatus
			 * @override
			 */
			setStatus: function(status) {
				const previousStatus = this.status;
				if (!this._initialStatus && status !== this._initialStatus) {
					this._initialStatus = previousStatus;
				}
				this.callParent(arguments);
			},

			/**
			 * @inheritdoc Terrasoft.BaseSchemaManagerItem#applyChanges
			 * @override
			 */
			applyChanges: function(callback, scope) {
				const isNew = this._initialStatus === Terrasoft.ModificationStatus.NEW;
				this.callParent([function(response) {
					if (response.success) {
						this._saveAgentSchema(isNew, function(agentSchemaResponse) {
							callback.call(scope, agentSchemaResponse ?? response);
						}, this);
					} else {
						callback.call(scope, response);
					}
				}, this]);
			},
		});

		return Terrasoft.WorkflowAgentSchemaManagerItem;
	});
